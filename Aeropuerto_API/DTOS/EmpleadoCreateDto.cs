using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class EmpleadoCreateDto
    {
        [Required]
        public string NumeroEmpleado { get; set; } = string.Empty;

        [Required]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        public string? DPI { get; set; }
        public string? NIT { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        [Required]
        public DateTime FechaContratacion { get; set; }

        [Required]
        public int IdPuesto { get; set; }

        [Required]
        public int IdDepartamento { get; set; }

        [Required]
        public decimal SalarioActual { get; set; }

        [Required]
        public string TipoContrato { get; set; } = string.Empty;

        public string? Estado { get; set; }
    }
}