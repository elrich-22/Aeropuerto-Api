using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class CategoriaRepuestoCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }
    }
}