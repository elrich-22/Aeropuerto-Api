using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class MetodoPagoCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Estado { get; set; }

        [Range(0, 100, ErrorMessage = "La comisión debe estar entre 0 y 100.")]
        public decimal? ComisionPorcentaje { get; set; }
    }
}