using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class DetalleVentaBoletoCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdVenta debe ser mayor a 0.")]
        public int IdVenta { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdReserva debe ser mayor a 0.")]
        public int IdReserva { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio base no puede ser negativo.")]
        public decimal? PrecioBase { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los cargos adicionales no pueden ser negativos.")]
        public decimal? CargosAdicionales { get; set; }
    }
}