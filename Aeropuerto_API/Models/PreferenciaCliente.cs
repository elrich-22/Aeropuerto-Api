namespace AeropuertoAPI.Models
{
    public class PreferenciaCliente
    {
        public int IdPreferencia { get; set; }
        public int? IdPasajero { get; set; }
        public string? TipoPreferencia { get; set; }
        public string? ValorPreferencia { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
