namespace AeropuertoAPI.Models
{
    public class TransaccionPago
    {
        public int Id { get; set; }
        public int IdReserva { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public DateTime FechaTransaccion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? NumeroAutorizacion { get; set; }
        public string? ReferenciaExterna { get; set; }
        public string? IpCliente { get; set; }
        public string? DetallesTarjeta { get; set; }
    }
}