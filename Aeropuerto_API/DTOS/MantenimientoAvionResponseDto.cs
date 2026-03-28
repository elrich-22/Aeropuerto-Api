namespace AeropuertoAPI.DTOs
{
    public class MantenimientoAvionResponseDto
    {
        public int Id { get; set; }
        public int IdAvion { get; set; }
        public string TipoMantenimiento { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinEstimada { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public string? DescripcionTrabajo { get; set; }
        public int IdEmpleadoResponsable { get; set; }
        public decimal? CostoManoObra { get; set; }
        public decimal? CostoRepuestos { get; set; }
        public decimal? CostoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}