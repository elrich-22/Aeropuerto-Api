namespace AeropuertoAPI.Models
{
    public class OrdenCompraRepuesto
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public int IdProveedor { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime? FechaEntregaEsperada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public decimal? MontoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int IdEmpleadoSolicita { get; set; }
        public string? Observaciones { get; set; }
    }
}