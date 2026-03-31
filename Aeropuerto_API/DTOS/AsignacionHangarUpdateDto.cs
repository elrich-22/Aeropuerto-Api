using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AsignacionHangarUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdHangar debe ser mayor a 0.")]
        public int IdHangar { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAvion debe ser mayor a 0.")]
        public int IdAvion { get; set; }

        [Required]
        public DateTime FechaEntrada { get; set; }

        public DateTime? FechaSalidaProgramada { get; set; }

        public DateTime? FechaSalidaReal { get; set; }

        [StringLength(250)]
        public string? Motivo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo por hora no puede ser negativo.")]
        public decimal? CostoHora { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo total no puede ser negativo.")]
        public decimal? CostoTotal { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}