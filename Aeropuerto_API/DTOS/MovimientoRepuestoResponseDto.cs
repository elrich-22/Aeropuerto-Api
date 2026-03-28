namespace AeropuertoAPI.DTOs
{
    public class MovimientoRepuestoResponseDto
    {
        public int Id { get; set; }
        public int IdRepuesto { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int IdEmpleado { get; set; }
        public string? Motivo { get; set; }
        public string? Referencia { get; set; }
    }
}