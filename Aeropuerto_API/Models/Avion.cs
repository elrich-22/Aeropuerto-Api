namespace AeropuertoAPI.Models
{
    public class Avion
    {
        public int Id { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public int IdModelo { get; set; }
        public int IdAerolinea { get; set; }
        public int? AnioFabricacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime? UltimaRevision { get; set; }
        public DateTime? ProximaRevision { get; set; }
        public int HorasVuelo { get; set; }
    }
}