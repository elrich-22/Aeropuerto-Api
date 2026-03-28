namespace AeropuertoAPI.DTOs
{
    public class ProgramaVueloResponseDto
    {
        public int Id { get; set; }
        public string NumeroVuelo { get; set; } = string.Empty;
        public int IdAerolinea { get; set; }
        public int IdAeropuertoOrigen { get; set; }
        public int IdAeropuertoDestino { get; set; }
        public string HoraSalidaProgramada { get; set; } = string.Empty;
        public string HoraLlegadaProgramada { get; set; } = string.Empty;
        public int? DuracionEstimada { get; set; }
        public string? TipoVuelo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }
    }
}