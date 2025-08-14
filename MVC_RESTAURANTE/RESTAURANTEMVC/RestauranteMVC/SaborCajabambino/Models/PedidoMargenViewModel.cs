namespace SaborCajabambino.Models
{
    public class PedidoMargenViewModel
    {
        public string Cliente { get; set; }
        public string Nombre { get; set; }
        public string Empleado { get; set; }
        public int Mesa { get; set; }
        public decimal PrecioVentaTotal { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal Margen { get; set; }
    }
}
