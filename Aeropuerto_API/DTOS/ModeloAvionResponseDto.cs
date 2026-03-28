namespace AeropuertoAPI.DTOs
{
    public class ModeloAvionResponseDto
    {
        public int Id { get; set; }
        public string NombreModelo { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public int CapacidadPasajeros { get; set; }
        public int? CapacidadCarga { get; set; }
        public int? AlcanceKm { get; set; }
        public int? VelocidadCrucero { get; set; }
        public int? AnioIntroduccion { get; set; }
        public string? TipoMotor { get; set; }
    }
}