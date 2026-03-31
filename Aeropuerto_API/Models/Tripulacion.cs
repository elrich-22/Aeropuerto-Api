namespace AeropuertoAPI.Models
{
    public class Tripulacion
    {
        public int Id { get; set; }
        public int IdVuelo { get; set; }
        public int IdEmpleado { get; set; }
        public string Rol { get; set; } = string.Empty;
        public decimal? HorasVuelo { get; set; }
    }
}