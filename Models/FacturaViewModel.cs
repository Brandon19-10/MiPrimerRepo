using ProyectoFinal.Models;

namespace ProyectoFinal.Models
{
    public class FacturaViewModel
    {
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
    public class Factura
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public ICollection<FacturaDetalle> Detalles { get; set; }
    }

    public class FacturaDetalle
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public Factura Factura { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
    }
}
