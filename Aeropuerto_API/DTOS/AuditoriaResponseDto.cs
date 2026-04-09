namespace AeropuertoAPI.DTOs
{
    public class AuditoriaResponseDto
    {
        public int IdAuditoria { get; set; }
        public string? TablaAfectada { get; set; }
        public string? Operacion { get; set; }
        public string? Usuario { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? IpAddress { get; set; }
        public string? DatosAnteriores { get; set; }
        public string? DatosNuevos { get; set; }
    }
}
