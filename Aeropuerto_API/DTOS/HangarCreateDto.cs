using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class HangarCreateDto
    {
        [Required]
        [StringLength(20)]
        public string CodigoHangar { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int IdAeropuerto { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La capacidad no puede ser negativa.")]
        public int? CapacidadAviones { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El área no puede ser negativa.")]
        public decimal? AreaM2 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La altura máxima no puede ser negativa.")]
        public decimal? AlturaMaxima { get; set; }

        [StringLength(30)]
        public string? Tipo { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}