namespace AeropuertoAPI.Models
{
    public class ClickDestino
    {
        public int IdClick { get; set; }
        public int? IdSesion { get; set; }
        public int? IdAeropuertoDestino { get; set; }
        public DateTime? FechaClick { get; set; }
        public string? OrigenBusqueda { get; set; }
        public DateTime? FechaViajeBuscada { get; set; }
        public decimal? NumeroPasajeros { get; set; }
        public string? ClaseBuscada { get; set; }
    }
}
