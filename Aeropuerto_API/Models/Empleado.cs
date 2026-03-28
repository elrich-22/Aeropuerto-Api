namespace AeropuertoAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NumeroEmpleado { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? DPI { get; set; }
        public string? NIT { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public DateTime FechaContratacion { get; set; }
        public int IdPuesto { get; set; }
        public int IdDepartamento { get; set; }
        public decimal SalarioActual { get; set; }
        public string TipoContrato { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}