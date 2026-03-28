using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AerolineaUpdateDto
    {
        [Required]
        [StringLength(10)]
        public string CodigoAerolinea { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PaisOrigen { get; set; } = string.Empty;

        [StringLength(3)]
        public string? CodigoIata { get; set; }

        [StringLength(4)]
        public string? CodigoIcao { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? SitioWeb { get; set; }
    }
}