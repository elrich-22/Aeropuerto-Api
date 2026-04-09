namespace AeropuertoAPI.DTOs
{
    public class CarritoCompraResponseDto
    {
        public int IdCarrito { get; set; }
        public int? IdPasajero { get; set; }
        public string? SesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string? Estado { get; set; }
    }
}
