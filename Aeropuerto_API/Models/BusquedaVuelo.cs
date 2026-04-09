namespace AeropuertoAPI.Models
{
    public class BusquedaVuelo
    {
        public int IdBusqueda { get; set; }
        public int? IdSesion { get; set; }
        public int? IdAeropuertoOrigen { get; set; }
        public int? IdAeropuertoDestino { get; set; }
        public DateTime? FechaIda { get; set; }
        public DateTime? FechaVuelta { get; set; }
        public decimal? NumeroPasajeros { get; set; }
        public string? Clase { get; set; }
        public DateTime? FechaBusqueda { get; set; }
        public string? SeConvirtioCompra { get; set; }
    }
}
