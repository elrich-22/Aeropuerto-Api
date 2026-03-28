using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodoPagoController : ControllerBase
    {
        private readonly MetodoPagoService _metodoPagoService;

        public MetodoPagoController(MetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var metodos = await _metodoPagoService.ObtenerTodosAsync();
                return Ok(metodos);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los métodos de pago.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los métodos de pago.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var metodo = await _metodoPagoService.ObtenerPorIdAsync(id);

                if (metodo == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el método de pago."
                    });

                return Ok(metodo);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el método de pago.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el método de pago.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MetodoPagoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _metodoPagoService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el método de pago."
                    });

                return Ok(new
                {
                    mensaje = "Método de pago creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el método de pago.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el método de pago.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] MetodoPagoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _metodoPagoService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el método de pago a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Método de pago actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el método de pago.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el método de pago.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _metodoPagoService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el método de pago a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Método de pago eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el método de pago.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el método de pago.",
                    motivo = ex.Message
                });
            }
        }
    }
}