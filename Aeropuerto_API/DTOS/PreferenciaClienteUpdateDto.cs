using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class PreferenciaClienteUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int? IdPasajero { get; set; }

        [StringLength(50)]
        [RegularExpression("^(ASIENTO_VENTANA|ASIENTO_PASILLO|COMIDA_ESPECIAL|EQUIPAJE_EXTRA|ASISTENCIA_ESPECIAL)$", ErrorMessage = "El tipo de preferencia no es válido.")]
        public string? TipoPreferencia { get; set; }

        [StringLength(200)]
        public string? ValorPreferencia { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
