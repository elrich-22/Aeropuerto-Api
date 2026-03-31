using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class EscalaTecnicaCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdProgramaVuelo debe ser mayor a 0.")]
        public int IdProgramaVuelo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int IdAeropuerto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El número de orden debe ser mayor a 0.")]
        public int NumeroOrden { get; set; }

        [StringLength(10)]
        public string? HoraLlegadaEstimada { get; set; }

        [StringLength(10)]
        public string? HoraSalidaEstimada { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La duración de escala no puede ser negativa.")]
        public int? DuracionEscala { get; set; }
    }
}