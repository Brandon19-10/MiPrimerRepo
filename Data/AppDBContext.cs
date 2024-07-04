using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Congifuracion de la entidad usuarios
            modelBuilder.Entity<Usuarios>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.Clave).HasMaxLength(50);
                tb.Property(col => col.Usuario).HasMaxLength(50);
                tb.Property(col => col.Rol).HasMaxLength(50);
            });
            modelBuilder.Entity<Usuarios>().ToTable("usuario");
            
            //Configuracion de Producto
            modelBuilder.Entity<Producto>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Nombre).HasMaxLength(100);
                tb.Property(col => col.IdTipo).HasColumnName("id_tipo"); // Nombre de columna en la base de datos
                tb.Property(col => col.Iva);
                tb.Property(col => col.CodigoBarras).HasMaxLength(50).HasColumnName("codigo_barras"); // Nombre de columna en la base de datos
            });
            modelBuilder.Entity<Producto>().ToTable("producto");

            //Configuracion Cliente
            modelBuilder.Entity<Cliente>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Nombres).HasMaxLength(100);
                tb.Property(col => col.Apellidos).HasMaxLength(100);
                tb.Property(col => col.Cedula).HasMaxLength(20);
                tb.Property(col => col.Direccion).HasMaxLength(200);
                tb.Property(col => col.Telefono).HasMaxLength(15);
            });
            modelBuilder.Entity<Cliente>().ToTable("cliente");


        }
    }
}
