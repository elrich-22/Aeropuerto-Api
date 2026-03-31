using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class RepuestoUtilizadoUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdMantenimiento debe ser mayor a 0.")]
        public int IdMantenimiento { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdRepuesto debe ser mayor a 0.")]
        public int IdRepuesto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }
}