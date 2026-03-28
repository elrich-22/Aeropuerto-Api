using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class VentaBoletoUpdateDto
    {
        [Required]
        [StringLength(30)]
        public string NumeroVenta { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdPuntoVenta debe ser mayor a 0.")]
        public int IdPuntoVenta { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleadoVendedor debe ser mayor a 0.")]
        public int IdEmpleadoVendedor { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int IdPasajero { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
        public decimal MontoSubtotal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los impuestos no pueden ser negativos.")]
        public decimal Impuestos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los descuentos no pueden ser negativos.")]
        public decimal Descuentos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo.")]
        public decimal MontoTotal { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdMetodoPago debe ser mayor a 0.")]
        public int IdMetodoPago { get; set; }

        [Required]
        [StringLength(20)]
        public string CanalVenta { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}