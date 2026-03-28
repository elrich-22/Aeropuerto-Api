using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PasajeroCreateDto
    {
        [Required]
        [StringLength(50)]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string TipoDocumento { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string Nacionalidad { get; set; } = string.Empty;

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}