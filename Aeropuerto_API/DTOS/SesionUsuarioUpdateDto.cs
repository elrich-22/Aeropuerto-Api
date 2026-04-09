using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class SesionUsuarioUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string SesionId { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int? IdPasajero { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        [StringLength(100)]
        public string? Navegador { get; set; }

        [StringLength(50)]
        public string? SistemaOperativo { get; set; }

        [StringLength(50)]
        public string? Dispositivo { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La duración en segundos no puede ser negativa.")]
        public decimal? DuracionSegundos { get; set; }
    }
}
