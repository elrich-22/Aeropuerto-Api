using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PromocionCreateDto
    {
        [StringLength(50)]
        public string? CodigoPromocion { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [StringLength(20)]
        [RegularExpression("^(PORCENTAJE|MONTO_FIJO)$", ErrorMessage = "El tipo de descuento debe ser PORCENTAJE o MONTO_FIJO.")]
        public string? TipoDescuento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor del descuento no puede ser negativo.")]
        public decimal? ValorDescuento { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los usos máximos no pueden ser negativos.")]
        public decimal? UsosMaximos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los usos actuales no pueden ser negativos.")]
        public decimal? UsosActuales { get; set; }

        [StringLength(20)]
        [RegularExpression("^(ACTIVA|INACTIVA|VENCIDA)$", ErrorMessage = "El estado debe ser ACTIVA, INACTIVA o VENCIDA.")]
        public string? Estado { get; set; }
    }
}
