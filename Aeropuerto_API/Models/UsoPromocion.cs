namespace AeropuertoAPI.Models
{
    public class UsoPromocion
    {
        public int IdUso { get; set; }
        public int? IdPromocion { get; set; }
        public int? IdReserva { get; set; }
        public DateTime? FechaUso { get; set; }
        public decimal? MontoDescuento { get; set; }
    }
}
