using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class DepartamentoCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int IdAeropuerto { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}