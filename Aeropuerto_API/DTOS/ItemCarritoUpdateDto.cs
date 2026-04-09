using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ItemCarritoUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdCarrito debe ser mayor a 0.")]
        public int? IdCarrito { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdVuelo debe ser mayor a 0.")]
        public int? IdVuelo { get; set; }

        [StringLength(10)]
        public string? NumeroAsiento { get; set; }

        [StringLength(20)]
        [RegularExpression("^(ECONOMICA|EJECUTIVA|PRIMERA)$", ErrorMessage = "La clase debe ser ECONOMICA, EJECUTIVA o PRIMERA.")]
        public string? Clase { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public decimal? PrecioUnitario { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal? Cantidad { get; set; }
    }
}
