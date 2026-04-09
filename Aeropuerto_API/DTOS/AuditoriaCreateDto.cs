using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class AuditoriaCreateDto
    {
        [StringLength(50)]
        public string? TablaAfectada { get; set; }

        [StringLength(10)]
        [RegularExpression("^(INSERT|UPDATE|DELETE)$", ErrorMessage = "La operación debe ser INSERT, UPDATE o DELETE.")]
        public string? Operacion { get; set; }

        [StringLength(50)]
        public string? Usuario { get; set; }

        public DateTime? FechaHora { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        public string? DatosAnteriores { get; set; }

        public string? DatosNuevos { get; set; }
    }
}
