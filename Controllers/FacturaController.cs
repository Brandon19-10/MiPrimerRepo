using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class FacturaController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public FacturaController(AppDBContext context)
        {
            _appDbContext = context;
        }

        // GET: Factura/Create
        public async Task<IActionResult> Create()
        {
            var model = new FacturaViewModel
            {
                Clientes = await _appDbContext.Clientes.ToListAsync(),
                Productos = await _appDbContext.Productos.ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Factura model)
        {
            if (ModelState.IsValid)
            {
                var factura = new Factura
                {
                    IdCliente = model.IdCliente,
                    SubTotal = model.SubTotal,
                    Iva = model.Iva,
                    Total = model.Total,
                    Fecha = DateTime.Now
                };

                _appDbContext.Facturas.Add(factura);
                await _appDbContext.SaveChangesAsync();

                foreach (var detalle in model.Detalles)
                {
                    var facturaDetalle = new FacturaDetalle
                    {
                        IdFactura = factura.Id,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        Precio = detalle.Precio,
                        Descuento = detalle.Descuento,
                        Total = detalle.Total
                    };

                    _appDbContext.FacturaDetalles.Add(facturaDetalle);
                }

                await _appDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
