namespace AeropuertoAPI.Models
{
    public class Pasajero
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? FechaRegistro { get; set; }
    }
}