namespace AeropuertoAPI.Models
{
    public class LicenciaEmpleado
    {
        public int IdLicencia { get; set; }
        public int? IdEmpleado { get; set; }
        public string? TipoLicencia { get; set; }
        public string? NumeroLicencia { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? AutoridadEmisora { get; set; }
        public string? Estado { get; set; }
    }
}
