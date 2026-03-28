using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ReservaCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdVuelo debe ser mayor a 0.")]
        public int IdVuelo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int IdPasajero { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroAsiento { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Clase { get; set; } = string.Empty;

        [Required]
        public DateTime FechaReserva { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El equipaje facturado no puede ser negativo.")]
        public int? EquipajeFacturado { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El peso del equipaje no puede ser negativo.")]
        public decimal? PesoEquipaje { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La tarifa pagada no puede ser negativa.")]
        public decimal TarifaPagada { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoReserva { get; set; } = string.Empty;
    }
}