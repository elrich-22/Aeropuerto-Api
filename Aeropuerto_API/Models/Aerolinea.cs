namespace AeropuertoAPI.Models
{
    public class Aerolinea
    {
        public int Id { get; set; }
        public string CodigoAerolinea { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public string? CodigoIata { get; set; }
        public string? CodigoIcao { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? SitioWeb { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}