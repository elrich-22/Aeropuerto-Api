namespace AeropuertoAPI.DTOs
{
    public class RepuestoResponseDto
    {
        public int Id { get; set; }
        public string CodigoRepuesto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdModeloAvion { get; set; }
        public string? NumeroParteFabricante { get; set; }
        public int? StockMinimo { get; set; }
        public int? StockActual { get; set; }
        public int? StockMaximo { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string? UbicacionBodega { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}