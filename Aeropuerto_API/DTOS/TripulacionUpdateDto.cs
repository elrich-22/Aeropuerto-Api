using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class TripulacionUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdVuelo debe ser mayor a 0.")]
        public int IdVuelo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleado debe ser mayor a 0.")]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(50)]
        public string Rol { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Las horas de vuelo no pueden ser negativas.")]
        public decimal? HorasVuelo { get; set; }
    }
}