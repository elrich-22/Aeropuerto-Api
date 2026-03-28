using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AeropuertoController : ControllerBase
    {
        private readonly AeropuertoService _aeropuertoService;

        public AeropuertoController(AeropuertoService aeropuertoService)
        {
            _aeropuertoService = aeropuertoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var aeropuertos = await _aeropuertoService.ObtenerTodosAsync();
                return Ok(aeropuertos);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los aeropuertos.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los aeropuertos.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var aeropuerto = await _aeropuertoService.ObtenerPorIdAsync(id);

                if (aeropuerto == null)
                    return NotFound(new { mensaje = "No se encontró el aeropuerto." });

                return Ok(aeropuerto);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el aeropuerto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el aeropuerto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AeropuertoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _aeropuertoService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new { mensaje = "No se pudo crear el aeropuerto." });

                return Ok(new { mensaje = "Aeropuerto creado correctamente." });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el aeropuerto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el aeropuerto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AeropuertoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _aeropuertoService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new { mensaje = "No se encontró el aeropuerto a actualizar." });

                return Ok(new { mensaje = "Aeropuerto actualizado correctamente." });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el aeropuerto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el aeropuerto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _aeropuertoService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new { mensaje = "No se encontró el aeropuerto a eliminar." });

                return Ok(new { mensaje = "Aeropuerto eliminado correctamente." });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el aeropuerto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el aeropuerto.",
                    motivo = ex.Message
                });
            }
        }
    }
}