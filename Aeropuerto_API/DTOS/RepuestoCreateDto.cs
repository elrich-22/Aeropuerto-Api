using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class RepuestoCreateDto
    {
        [Required]
        [StringLength(30)]
        public string CodigoRepuesto { get; set; } = string.Empty;

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdCategoria debe ser mayor a 0.")]
        public int IdCategoria { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdModeloAvion debe ser mayor a 0.")]
        public int IdModeloAvion { get; set; }

        [StringLength(50)]
        public string? NumeroParteFabricante { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
        public int? StockMinimo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock actual no puede ser negativo.")]
        public int? StockActual { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock máximo no puede ser negativo.")]
        public int? StockMaximo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public decimal? PrecioUnitario { get; set; }

        [StringLength(50)]
        public string? UbicacionBodega { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}