namespace AeropuertoAPI.Models
{
    public class DetalleVentaBoleto
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdReserva { get; set; }
        public decimal? PrecioBase { get; set; }
        public decimal? CargosAdicionales { get; set; }
    }
}