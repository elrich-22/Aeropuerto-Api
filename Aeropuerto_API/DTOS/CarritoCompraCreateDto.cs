using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class CarritoCompraCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int? IdPasajero { get; set; }

        [StringLength(100)]
        public string? SesionId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        [StringLength(20)]
        [RegularExpression("^(ACTIVO|COMPLETADO|ABANDONADO|EXPIRADO)$", ErrorMessage = "El estado debe ser ACTIVO, COMPLETADO, ABANDONADO o EXPIRADO.")]
        public string? Estado { get; set; }
    }
}
