using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class LicenciaEmpleadoUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleado debe ser mayor a 0.")]
        public int? IdEmpleado { get; set; }

        [StringLength(50)]
        public string? TipoLicencia { get; set; }

        [StringLength(50)]
        public string? NumeroLicencia { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [StringLength(100)]
        public string? AutoridadEmisora { get; set; }

        [StringLength(20)]
        [RegularExpression("^(VIGENTE|VENCIDA|SUSPENDIDA|RENOVADA)$", ErrorMessage = "El estado debe ser VIGENTE, VENCIDA, SUSPENDIDA o RENOVADA.")]
        public string? Estado { get; set; }
    }
}
