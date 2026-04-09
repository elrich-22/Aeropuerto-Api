namespace AeropuertoAPI.Models
{
    public class Arresto
    {
        public int IdArresto { get; set; }
        public int? IdPasajero { get; set; }
        public int? IdVuelo { get; set; }
        public int? IdAeropuerto { get; set; }
        public DateTime? FechaHoraArresto { get; set; }
        public string? Motivo { get; set; }
        public string? AutoridadCargo { get; set; }
        public string? DescripcionIncidente { get; set; }
        public string? UbicacionArresto { get; set; }
        public string? EstadoCaso { get; set; }
        public string? NumeroExpediente { get; set; }
    }
}
