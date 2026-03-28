using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ModeloAvionUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreModelo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Fabricante { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad de pasajeros debe ser mayor a 0.")]
        public int CapacidadPasajeros { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La capacidad de carga no puede ser negativa.")]
        public int? CapacidadCarga { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El alcance no puede ser negativo.")]
        public int? AlcanceKm { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La velocidad crucero no puede ser negativa.")]
        public int? VelocidadCrucero { get; set; }

        [Range(1900, 2100, ErrorMessage = "El año de introducción no es válido.")]
        public int? AnioIntroduccion { get; set; }

        [StringLength(50)]
        public string? TipoMotor { get; set; }
    }
}