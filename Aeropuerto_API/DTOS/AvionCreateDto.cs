using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AvionCreateDto
    {
        [Required]
        [StringLength(20)]
        public string Matricula { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdModelo debe ser mayor a 0.")]
        public int IdModelo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAerolinea debe ser mayor a 0.")]
        public int IdAerolinea { get; set; }

        [Range(1900, 2100, ErrorMessage = "El año de fabricación no es válido.")]
        public int? AnioFabricacion { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public DateTime? UltimaRevision { get; set; }

        public DateTime? ProximaRevision { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Las horas de vuelo no pueden ser negativas.")]
        public int? HorasVuelo { get; set; }
    }
}