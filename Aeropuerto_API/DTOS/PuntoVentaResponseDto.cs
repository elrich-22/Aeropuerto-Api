namespace AeropuertoAPI.DTOs
{
    public class PuntoVentaResponseDto
    {
        public int Id { get; set; }
        public string CodigoPunto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int IdAeropuerto { get; set; }
        public string? Ubicacion { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}