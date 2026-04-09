using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class UsoPromocionUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdPromocion debe ser mayor a 0.")]
        public int? IdPromocion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdReserva debe ser mayor a 0.")]
        public int? IdReserva { get; set; }

        public DateTime? FechaUso { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto de descuento no puede ser negativo.")]
        public decimal? MontoDescuento { get; set; }
    }
}
