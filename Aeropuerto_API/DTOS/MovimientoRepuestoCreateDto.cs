using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class MovimientoRepuestoCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdRepuesto debe ser mayor a 0.")]
        public int IdRepuesto { get; set; }

        [Required]
        [StringLength(30)]
        public string TipoMovimiento { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaMovimiento { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleado debe ser mayor a 0.")]
        public int IdEmpleado { get; set; }

        [StringLength(200)]
        public string? Motivo { get; set; }

        [StringLength(100)]
        public string? Referencia { get; set; }
    }
}