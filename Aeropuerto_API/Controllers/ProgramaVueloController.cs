using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaVueloController : ControllerBase
    {
        private readonly ProgramaVueloService _programaVueloService;

        public ProgramaVueloController(ProgramaVueloService programaVueloService)
        {
            _programaVueloService = programaVueloService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var programas = await _programaVueloService.ObtenerTodosAsync();
                return Ok(programas);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los programas de vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los programas de vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var programa = await _programaVueloService.ObtenerPorIdAsync(id);

                if (programa == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el programa de vuelo."
                    });

                return Ok(programa);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el programa de vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el programa de vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProgramaVueloCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.IdAeropuertoOrigen == dto.IdAeropuertoDestino)
            {
                return BadRequest(new
                {
                    mensaje = "El aeropuerto de origen y destino no pueden ser el mismo."
                });
            }

            try
            {
                var creado = await _programaVueloService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el programa de vuelo."
                    });

                return Ok(new
                {
                    mensaje = "Programa de vuelo creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el programa de vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el programa de vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProgramaVueloUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.IdAeropuertoOrigen == dto.IdAeropuertoDestino)
            {
                return BadRequest(new
                {
                    mensaje = "El aeropuerto de origen y destino no pueden ser el mismo."
                });
            }

            try
            {
                var actualizado = await _programaVueloService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el programa de vuelo a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Programa de vuelo actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el programa de vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el programa de vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _programaVueloService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el programa de vuelo a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Programa de vuelo eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el programa de vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el programa de vuelo.",
                    motivo = ex.Message
                });
            }
        }
    }
}