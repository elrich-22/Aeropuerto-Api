using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class DetalleOrdenCompraCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdOrdenCompra debe ser mayor a 0.")]
        public int? IdOrdenCompra { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdRepuesto debe ser mayor a 0.")]
        public int? IdRepuesto { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal? Cantidad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public decimal? PrecioUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
        public decimal? Subtotal { get; set; }
    }
}
