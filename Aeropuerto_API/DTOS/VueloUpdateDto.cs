using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class VueloUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdPrograma debe ser mayor a 0.")]
        public int IdPrograma { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAvion debe ser mayor a 0.")]
        public int IdAvion { get; set; }

        [Required]
        public DateTime FechaVuelo { get; set; }

        public DateTime? HoraSalidaReal { get; set; }

        public DateTime? HoraLlegadaReal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Las plazas ocupadas no pueden ser negativas.")]
        public int PlazasOcupadas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Las plazas vacías no pueden ser negativas.")]
        public int? PlazasVacias { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public DateTime? FechaReprogramacion { get; set; }

        [StringLength(500)]
        public string? MotivoCancelacion { get; set; }

        [StringLength(10)]
        public string? PuertaEmbarque { get; set; }

        [StringLength(10)]
        public string? Terminal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El retraso no puede ser negativo.")]
        public int RetrasoMinutos { get; set; }
    }
}