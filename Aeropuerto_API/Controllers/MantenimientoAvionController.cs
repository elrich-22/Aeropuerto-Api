using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MantenimientoAvionController : ControllerBase
    {
        private readonly MantenimientoAvionService _service;

        public MantenimientoAvionController(MantenimientoAvionService service)
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
                    mensaje = "Error de Oracle al obtener los mantenimientos de avión.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los mantenimientos de avión.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var mantenimiento = await _service.ObtenerPorIdAsync(id);

                if (mantenimiento == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el mantenimiento de avión."
                    });

                return Ok(mantenimiento);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el mantenimiento de avión.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el mantenimiento de avión.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MantenimientoAvionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el mantenimiento de avión."
                    });

                return Ok(new
                {
                    mensaje = "Mantenimiento de avión creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el mantenimiento de avión.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el mantenimiento de avión.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] MantenimientoAvionUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el mantenimiento de avión a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Mantenimiento de avión actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el mantenimiento de avión.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el mantenimiento de avión.",
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
                        mensaje = "No se encontró el mantenimiento de avión a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Mantenimiento de avión eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el mantenimiento de avión.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el mantenimiento de avión.",
                    motivo = ex.Message
                });
            }
        }
    }
}