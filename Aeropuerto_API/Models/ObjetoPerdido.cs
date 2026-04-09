namespace AeropuertoAPI.Models
{
    public class ObjetoPerdido
    {
        public int IdObjeto { get; set; }
        public int? IdVuelo { get; set; }
        public int? IdAeropuerto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string? UbicacionEncontrado { get; set; }
        public string? Estado { get; set; }
        public string? NombreReportante { get; set; }
        public string? ContactoReportante { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? NombreReclamante { get; set; }
    }
}
