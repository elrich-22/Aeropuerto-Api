namespace AeropuertoAPI.DTOs
{
    public class PlanillaResponseDto
    {
        public int IdPlanilla { get; set; }
        public int? IdEmpleado { get; set; }
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public decimal? SalarioBase { get; set; }
        public decimal? Bonificaciones { get; set; }
        public decimal? HorasExtra { get; set; }
        public decimal? Deducciones { get; set; }
        public decimal? SalarioNeto { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? Estado { get; set; }
    }
}
