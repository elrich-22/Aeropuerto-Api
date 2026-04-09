using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ArrestoUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdPasajero debe ser mayor a 0.")]
        public int? IdPasajero { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdVuelo debe ser mayor a 0.")]
        public int? IdVuelo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int? IdAeropuerto { get; set; }

        public DateTime? FechaHoraArresto { get; set; }

        [StringLength(500)]
        public string? Motivo { get; set; }

        [StringLength(200)]
        public string? AutoridadCargo { get; set; }

        public string? DescripcionIncidente { get; set; }

        [StringLength(100)]
        [RegularExpression("^(TERMINAL|PUERTA|VUELO|ADUANA|MIGRACION|OTRO)$", ErrorMessage = "La ubicación del arresto no es válida.")]
        public string? UbicacionArresto { get; set; }

        [StringLength(20)]
        [RegularExpression("^(EN_PROCESO|CERRADO|REMITIDO)$", ErrorMessage = "El estado del caso debe ser EN_PROCESO, CERRADO o REMITIDO.")]
        public string? EstadoCaso { get; set; }

        [StringLength(50)]
        public string? NumeroExpediente { get; set; }
    }
}
