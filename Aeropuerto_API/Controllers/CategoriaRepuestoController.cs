using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaRepuestoController : ControllerBase
    {
        private readonly CategoriaRepuestoService _service;

        public CategoriaRepuestoController(CategoriaRepuestoService service)
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
                    mensaje = "Error de Oracle al obtener las categorías de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener las categorías de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var categoria = await _service.ObtenerPorIdAsync(id);

                if (categoria == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la categoría de repuesto."
                    });

                return Ok(categoria);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener la categoría de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener la categoría de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CategoriaRepuestoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear la categoría de repuesto."
                    });

                return Ok(new
                {
                    mensaje = "Categoría de repuesto creada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear la categoría de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear la categoría de repuesto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CategoriaRepuestoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la categoría de repuesto a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Categoría de repuesto actualizada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar la categoría de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar la categoría de repuesto.",
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
                        mensaje = "No se encontró la categoría de repuesto a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Categoría de repuesto eliminada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar la categoría de repuesto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar la categoría de repuesto.",
                    motivo = ex.Message
                });
            }
        }
    }
}