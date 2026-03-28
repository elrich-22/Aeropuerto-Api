using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AeropuertoUpdateDto
    {
        [Required]
        [StringLength(10)]
        public string CodigoAeropuerto { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Ciudad { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Pais { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ZonaHoraria { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [StringLength(20)]
        public string? Tipo { get; set; }

        [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
        public decimal? Latitud { get; set; }

        [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
        public decimal? Longitud { get; set; }

        [StringLength(3)]
        public string? CodigoIata { get; set; }

        [StringLength(4)]
        public string? CodigoIcao { get; set; }
    }
}