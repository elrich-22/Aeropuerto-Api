namespace AeropuertoAPI.DTOs
{
    public class VueloResponseDto
    {
        public int Id { get; set; }
        public int IdPrograma { get; set; }
        public int IdAvion { get; set; }
        public DateTime FechaVuelo { get; set; }
        public DateTime? HoraSalidaReal { get; set; }
        public DateTime? HoraLlegadaReal { get; set; }
        public int PlazasOcupadas { get; set; }
        public int? PlazasVacias { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime? FechaReprogramacion { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? PuertaEmbarque { get; set; }
        public string? Terminal { get; set; }
        public int RetrasoMinutos { get; set; }
    }
}