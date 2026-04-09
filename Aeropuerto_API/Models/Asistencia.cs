namespace AeropuertoAPI.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int? IdEmpleado { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public string? Tipo { get; set; }
        public string? Estado { get; set; }
    }
}
