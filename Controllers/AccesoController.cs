using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System.Security.Claims;
using System;
using ProyectoFinal.Data;
using ProyectoFinal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public AccesoController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _appDbContext.Usuarios.ToListAsync();
            return View(usuarios);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuarios? usuario_encontrado = await _appDbContext.Usuarios
                                                .Where(u =>
                                                u.Usuario == modelo.Usuario &&
                                                u.Clave == modelo.Clave
                                                ).FirstOrDefaultAsync();
            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se pudo encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuario_encontrado.Usuario)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
    }
}
