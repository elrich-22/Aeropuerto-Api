using System.ComponentModel.DataAnnotations;

namespace AeropuertoAPI.DTOs
{
    public class DiasVueloUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdProgramaVuelo debe ser mayor a 0.")]
        public int IdProgramaVuelo { get; set; }

        [Required]
        [Range(1, 7, ErrorMessage = "El día de semana debe estar entre 1 y 7.")]
        public int DiaSemana { get; set; }
    }
}