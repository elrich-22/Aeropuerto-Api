namespace AeropuertoAPI.Models
{
    public class Hangar
    {
        public int Id { get; set; }
        public string CodigoHangar { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int IdAeropuerto { get; set; }
        public int? CapacidadAviones { get; set; }
        public decimal? AreaM2 { get; set; }
        public decimal? AlturaMaxima { get; set; }
        public string? Tipo { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}