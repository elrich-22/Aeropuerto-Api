namespace AeropuertoAPI.DTOs
{
    public class VentaBoletoResponseDto
    {
        public int Id { get; set; }
        public string NumeroVenta { get; set; } = string.Empty;
        public int IdPuntoVenta { get; set; }
        public int IdEmpleadoVendedor { get; set; }
        public int IdPasajero { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoSubtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal MontoTotal { get; set; }
        public int IdMetodoPago { get; set; }
        public string CanalVenta { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}