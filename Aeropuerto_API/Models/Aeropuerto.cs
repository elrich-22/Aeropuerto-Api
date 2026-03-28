namespace AeropuertoAPI.Models
{
    public class Aeropuerto
    {
        public int Id { get; set; }
        public string CodigoAeropuerto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string? ZonaHoraria { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Tipo { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? CodigoIata { get; set; }
        public string? CodigoIcao { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}