using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class BusquedaVueloUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdSesion debe ser mayor a 0.")]
        public int? IdSesion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuertoOrigen debe ser mayor a 0.")]
        public int? IdAeropuertoOrigen { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuertoDestino debe ser mayor a 0.")]
        public int? IdAeropuertoDestino { get; set; }

        public DateTime? FechaIda { get; set; }

        public DateTime? FechaVuelta { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de pasajeros debe ser mayor a 0.")]
        public decimal? NumeroPasajeros { get; set; }

        [StringLength(20)]
        public string? Clase { get; set; }

        public DateTime? FechaBusqueda { get; set; }

        [StringLength(1)]
        [RegularExpression("^(S|N)$", ErrorMessage = "SeConvirtioCompra debe ser S o N.")]
        public string? SeConvirtioCompra { get; set; }
    }
}
