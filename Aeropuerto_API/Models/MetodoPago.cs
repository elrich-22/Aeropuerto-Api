namespace AeropuertoAPI.Models
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal? ComisionPorcentaje { get; set; }
    }
}