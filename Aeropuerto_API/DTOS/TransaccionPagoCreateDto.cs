using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class TransaccionPagoCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdReserva debe ser mayor a 0.")]
        public int IdReserva { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdMetodoPago debe ser mayor a 0.")]
        public int IdMetodoPago { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto total no puede ser negativo.")]
        public decimal MontoTotal { get; set; }

        [Required]
        [StringLength(10)]
        public string Moneda { get; set; } = string.Empty;

        [Required]
        public DateTime FechaTransaccion { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [StringLength(50)]
        public string? NumeroAutorizacion { get; set; }

        [StringLength(100)]
        public string? ReferenciaExterna { get; set; }

        [StringLength(50)]
        public string? IpCliente { get; set; }

        [StringLength(100)]
        public string? DetallesTarjeta { get; set; }
    }
}