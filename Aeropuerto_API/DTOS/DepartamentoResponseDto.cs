namespace AeropuertoAPI.DTOs
{
    public class DepartamentoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int IdAeropuerto { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}