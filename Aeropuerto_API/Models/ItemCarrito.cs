namespace AeropuertoAPI.Models
{
    public class ItemCarrito
    {
        public int IdItemCarrito { get; set; }
        public int? IdCarrito { get; set; }
        public int? IdVuelo { get; set; }
        public string? NumeroAsiento { get; set; }
        public string? Clase { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Cantidad { get; set; }
    }
}
