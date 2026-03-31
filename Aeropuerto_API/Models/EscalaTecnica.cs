namespace AeropuertoAPI.Models
{
    public class EscalaTecnica
    {
        public int Id { get; set; }
        public int IdProgramaVuelo { get; set; }
        public int IdAeropuerto { get; set; }
        public int NumeroOrden { get; set; }
        public string? HoraLlegadaEstimada { get; set; }
        public string? HoraSalidaEstimada { get; set; }
        public int? DuracionEscala { get; set; }
    }
}