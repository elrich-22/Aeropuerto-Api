using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaBoletoController : ControllerBase
    {
        private readonly VentaBoletoService _ventaBoletoService;

        public VentaBoletoController(VentaBoletoService ventaBoletoService)
        {
            _ventaBoletoService = ventaBoletoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var ventas = await _ventaBoletoService.ObtenerTodosAsync();
                return Ok(ventas);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener las ventas de boleto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener las ventas de boleto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var venta = await _ventaBoletoService.ObtenerPorIdAsync(id);

                if (venta == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la venta de boleto."
                    });

                return Ok(venta);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener la venta de boleto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener la venta de boleto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VentaBoletoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _ventaBoletoService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear la venta de boleto."
                    });

                return Ok(new
                {
                    mensaje = "Venta de boleto creada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear la venta de boleto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear la venta de boleto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] VentaBoletoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _ventaBoletoService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la venta de boleto a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Venta de boleto actualizada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar la venta de boleto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar la venta de boleto.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _ventaBoletoService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la venta de boleto a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Venta de boleto eliminada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar la venta de boleto.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar la venta de boleto.",
                    motivo = ex.Message
                });
            }
        }
    }
}