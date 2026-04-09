using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AsistenciaUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdEmpleado debe ser mayor a 0.")]
        public int? IdEmpleado { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }

        [Range(0, 999.99, ErrorMessage = "Las horas trabajadas deben estar entre 0 y 999.99.")]
        public decimal? HorasTrabajadas { get; set; }

        [StringLength(20)]
        [RegularExpression("^(REGULAR|EXTRA|FERIADO)$", ErrorMessage = "El tipo debe ser REGULAR, EXTRA o FERIADO.")]
        public string? Tipo { get; set; }

        [StringLength(20)]
        [RegularExpression("^(PRESENTE|AUSENTE|TARDE|PERMISO|VACACIONES)$", ErrorMessage = "El estado debe ser PRESENTE, AUSENTE, TARDE, PERMISO o VACACIONES.")]
        public string? Estado { get; set; }
    }
}
