using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ProveedorUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string NombreEmpresa { get; set; } = string.Empty;

        [StringLength(30)]
        public string? NIT { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        [StringLength(30)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? ContactoPrincipal { get; set; }

        [StringLength(50)]
        public string? Pais { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [Range(0, 5, ErrorMessage = "La calificación debe estar entre 0 y 5.")]
        public decimal? Calificacion { get; set; }
    }
}