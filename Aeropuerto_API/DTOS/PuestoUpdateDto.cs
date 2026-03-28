using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PuestoUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdDepartamento debe ser mayor a 0.")]
        public int IdDepartamento { get; set; }

        [StringLength(250)]
        public string? Descripcion { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario mínimo no puede ser negativo.")]
        public decimal? SalarioMinimo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario máximo no puede ser negativo.")]
        public decimal? SalarioMaximo { get; set; }

        [StringLength(2)]
        public string? RequiereLicencia { get; set; }
    }
}