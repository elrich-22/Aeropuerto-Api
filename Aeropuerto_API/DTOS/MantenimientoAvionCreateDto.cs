using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class MantenimientoAvionCreateDto
    {
        [Required]
        public int IdAvion { get; set; }

        [Required]
        public string TipoMantenimiento { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinEstimada { get; set; }

        public DateTime? FechaFinReal { get; set; }

        public string? DescripcionTrabajo { get; set; }

        [Required]
        public int IdEmpleadoResponsable { get; set; }

        public decimal? CostoManoObra { get; set; }

        public decimal? CostoRepuestos { get; set; }

        public decimal? CostoTotal { get; set; }

        public string? Estado { get; set; }
    }
}