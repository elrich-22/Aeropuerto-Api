namespace AeropuertoAPI.DTOs
{
    public class AsignacionHangarResponseDto
    {
        public int Id { get; set; }
        public int IdHangar { get; set; }
        public int IdAvion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalidaProgramada { get; set; }
        public DateTime? FechaSalidaReal { get; set; }
        public string? Motivo { get; set; }
        public decimal? CostoHora { get; set; }
        public decimal? CostoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}