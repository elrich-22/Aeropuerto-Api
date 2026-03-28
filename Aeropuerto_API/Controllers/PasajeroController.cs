using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasajeroController : ControllerBase
    {
        private readonly PasajeroService _pasajeroService;

        public PasajeroController(PasajeroService pasajeroService)
        {
            _pasajeroService = pasajeroService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var pasajeros = await _pasajeroService.ObtenerTodosAsync();
                return Ok(pasajeros);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los pasajeros.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los pasajeros.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var pasajero = await _pasajeroService.ObtenerPorIdAsync(id);

                if (pasajero == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el pasajero."
                    });

                return Ok(pasajero);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el pasajero.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el pasajero.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PasajeroCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _pasajeroService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el pasajero."
                    });

                return Ok(new
                {
                    mensaje = "Pasajero creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el pasajero.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el pasajero.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] PasajeroUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _pasajeroService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el pasajero a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Pasajero actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el pasajero.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el pasajero.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _pasajeroService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el pasajero a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Pasajero eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el pasajero.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el pasajero.",
                    motivo = ex.Message
                });
            }
        }
    }
}