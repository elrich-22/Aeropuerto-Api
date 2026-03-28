using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangarController : ControllerBase
    {
        private readonly HangarService _service;

        public HangarController(HangarService service)
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
                    mensaje = "Error de Oracle al obtener los hangares.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los hangares.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var hangar = await _service.ObtenerPorIdAsync(id);

                if (hangar == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el hangar."
                    });

                return Ok(hangar);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el hangar.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el hangar.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] HangarCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el hangar."
                    });

                return Ok(new
                {
                    mensaje = "Hangar creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el hangar.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el hangar.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] HangarUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el hangar a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Hangar actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el hangar.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el hangar.",
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
                        mensaje = "No se encontró el hangar a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Hangar eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el hangar.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el hangar.",
                    motivo = ex.Message
                });
            }
        }
    }
}