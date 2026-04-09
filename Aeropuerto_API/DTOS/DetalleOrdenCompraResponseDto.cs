namespace AeropuertoAPI.DTOs
{
    public class DetalleOrdenCompraResponseDto
    {
        public int IdDetalle { get; set; }
        public int? IdOrdenCompra { get; set; }
        public int? IdRepuesto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
