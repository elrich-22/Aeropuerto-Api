using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ClickDestinoUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdSesion debe ser mayor a 0.")]
        public int? IdSesion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuertoDestino debe ser mayor a 0.")]
        public int? IdAeropuertoDestino { get; set; }

        public DateTime? FechaClick { get; set; }

        [StringLength(10)]
        public string? OrigenBusqueda { get; set; }

        public DateTime? FechaViajeBuscada { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de pasajeros debe ser mayor a 0.")]
        public decimal? NumeroPasajeros { get; set; }

        [StringLength(20)]
        public string? ClaseBuscada { get; set; }
    }
}
