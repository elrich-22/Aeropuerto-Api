using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuestoController : ControllerBase
    {
        private readonly PuestoService _service;

        public PuestoController(PuestoService service)
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
                    mensaje = "Error de Oracle al obtener los puestos.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los puestos.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var puesto = await _service.ObtenerPorIdAsync(id);

                if (puesto == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el puesto."
                    });

                return Ok(puesto);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el puesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el puesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PuestoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el puesto."
                    });

                return Ok(new
                {
                    mensaje = "Puesto creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el puesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el puesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] PuestoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el puesto a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Puesto actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el puesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el puesto.",
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
                        mensaje = "No se encontró el puesto a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Puesto eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el puesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el puesto.",
                    motivo = ex.Message
                });
            }
        }
    }
}