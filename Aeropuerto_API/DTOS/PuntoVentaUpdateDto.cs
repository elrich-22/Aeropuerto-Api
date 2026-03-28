using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PuntoVentaUpdateDto
    {
        [Required]
        [StringLength(20)]
        public string CodigoPunto { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int IdAeropuerto { get; set; }

        [StringLength(150)]
        public string? Ubicacion { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}