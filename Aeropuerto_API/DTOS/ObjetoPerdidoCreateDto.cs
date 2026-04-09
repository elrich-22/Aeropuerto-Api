using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class ObjetoPerdidoCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El IdVuelo debe ser mayor a 0.")]
        public int? IdVuelo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdAeropuerto debe ser mayor a 0.")]
        public int? IdAeropuerto { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        public DateTime? FechaReporte { get; set; }

        [StringLength(200)]
        public string? UbicacionEncontrado { get; set; }

        [StringLength(20)]
        [RegularExpression("^(REPORTADO|RECLAMADO|ENTREGADO|ARCHIVADO)$", ErrorMessage = "El estado debe ser REPORTADO, RECLAMADO, ENTREGADO o ARCHIVADO.")]
        public string? Estado { get; set; }

        [StringLength(200)]
        public string? NombreReportante { get; set; }

        [StringLength(100)]
        public string? ContactoReportante { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [StringLength(200)]
        public string? NombreReclamante { get; set; }
    }
}
