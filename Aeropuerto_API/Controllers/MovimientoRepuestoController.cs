using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoRepuestoController : ControllerBase
    {
        private readonly MovimientoRepuestoService _service;

        public MovimientoRepuestoController(MovimientoRepuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var data = await _service.ObtenerTodosAsync();
                return Ok(data);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los movimientos de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los movimientos de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var movimiento = await _service.ObtenerPorIdAsync(id);

                if (movimiento == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el movimiento de repuesto."
                    });

                return Ok(movimiento);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el movimiento de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el movimiento de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MovimientoRepuestoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el movimiento de repuesto."
                    });

                return Ok(new
                {
                    mensaje = "Movimiento de repuesto creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el movimiento de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el movimiento de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] MovimientoRepuestoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el movimiento de repuesto a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Movimiento de repuesto actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el movimiento de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el movimiento de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var ok = await _service.EliminarAsync(id);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el movimiento de repuesto a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Movimiento de repuesto eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el movimiento de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el movimiento de repuesto.",
                    motivo = ex.Message
                });
            }
        }
    }
}