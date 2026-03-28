using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ProgramaVueloUpdateDto
    {
        [Required]
        [StringLength(20)]
        public string NumeroVuelo { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAerolinea debe ser mayor a 0.")]
        public int IdAerolinea { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuertoOrigen debe ser mayor a 0.")]
        public int IdAeropuertoOrigen { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuertoDestino debe ser mayor a 0.")]
        public int IdAeropuertoDestino { get; set; }

        [Required]
        [RegularExpression(@"^([01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "La hora de salida debe tener formato HH:mm.")]
        public string HoraSalidaProgramada { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^([01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "La hora de llegada debe tener formato HH:mm.")]
        public string HoraLlegadaProgramada { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "La duración estimada no puede ser negativa.")]
        public int? DuracionEstimada { get; set; }

        [StringLength(20)]
        public string? TipoVuelo { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}