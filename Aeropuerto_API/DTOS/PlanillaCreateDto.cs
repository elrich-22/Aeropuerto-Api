using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PlanillaCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleado debe ser mayor a 0.")]
        public int? IdEmpleado { get; set; }

        public DateTime? PeriodoInicio { get; set; }

        public DateTime? PeriodoFin { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario base no puede ser negativo.")]
        public decimal? SalarioBase { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las bonificaciones no pueden ser negativas.")]
        public decimal? Bonificaciones { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las horas extra no pueden ser negativas.")]
        public decimal? HorasExtra { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las deducciones no pueden ser negativas.")]
        public decimal? Deducciones { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario neto no puede ser negativo.")]
        public decimal? SalarioNeto { get; set; }

        public DateTime? FechaPago { get; set; }

        [StringLength(20)]
        [RegularExpression("^(PENDIENTE|PAGADA|ANULADA)$", ErrorMessage = "El estado debe ser PENDIENTE, PAGADA o ANULADA.")]
        public string? Estado { get; set; }
    }
}
