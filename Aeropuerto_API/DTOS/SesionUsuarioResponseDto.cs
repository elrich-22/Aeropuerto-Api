namespace AeropuertoAPI.DTOs
{
    public class SesionUsuarioResponseDto
    {
        public int IdSesion { get; set; }
        public string? SesionId { get; set; }
        public int? IdPasajero { get; set; }
        public string? IpAddress { get; set; }
        public string? Navegador { get; set; }
        public string? SistemaOperativo { get; set; }
        public string? Dispositivo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? DuracionSegundos { get; set; }
    }
}
