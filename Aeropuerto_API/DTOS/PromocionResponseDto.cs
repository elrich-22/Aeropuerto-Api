namespace AeropuertoAPI.DTOs
{
    public class PromocionResponseDto
    {
        public int IdPromocion { get; set; }
        public string? CodigoPromocion { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoDescuento { get; set; }
        public decimal? ValorDescuento { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? UsosMaximos { get; set; }
        public decimal? UsosActuales { get; set; }
        public string? Estado { get; set; }
    }
}
