namespace AeropuertoAPI.DTOs
{
    public class PuestoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdDepartamento { get; set; }
        public string? Descripcion { get; set; }
        public decimal? SalarioMinimo { get; set; }
        public decimal? SalarioMaximo { get; set; }
        public string? RequiereLicencia { get; set; }
    }
}