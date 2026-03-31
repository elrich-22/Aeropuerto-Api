using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class OrdenCompraRepuestoCreateDto
    {
        [Required]
        [StringLength(30)]
        public string NumeroOrden { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdProveedor debe ser mayor a 0.")]
        public int IdProveedor { get; set; }

        [Required]
        public DateTime FechaOrden { get; set; }

        public DateTime? FechaEntregaEsperada { get; set; }

        public DateTime? FechaEntregaReal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto total no puede ser negativo.")]
        public decimal? MontoTotal { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleadoSolicita debe ser mayor a 0.")]
        public int IdEmpleadoSolicita { get; set; }

        [StringLength(300)]
        public string? Observaciones { get; set; }
    }
}