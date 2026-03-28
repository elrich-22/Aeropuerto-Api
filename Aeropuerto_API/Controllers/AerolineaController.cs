using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AerolineaController : ControllerBase
    {
        private readonly AerolineaService _aerolineaService;

        public AerolineaController(AerolineaService aerolineaService)
        {
            _aerolineaService = aerolineaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var aerolineas = await _aerolineaService.ObtenerTodasAsync();
                return Ok(aerolineas);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener las aerolíneas.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener las aerolíneas.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var aerolinea = await _aerolineaService.ObtenerPorIdAsync(id);

                if (aerolinea == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la aerolínea."
                    });

                return Ok(aerolinea);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener la aerolínea.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener la aerolínea.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AerolineaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creada = await _aerolineaService.CrearAsync(dto);

                if (!creada)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear la aerolínea."
                    });

                return Ok(new
                {
                    mensaje = "Aerolínea creada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear la aerolínea.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear la aerolínea.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AerolineaUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizada = await _aerolineaService.ActualizarAsync(id, dto);

                if (!actualizada)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la aerolínea a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Aerolínea actualizada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar la aerolínea.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar la aerolínea.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminada = await _aerolineaService.EliminarAsync(id);

                if (!eliminada)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la aerolínea a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Aerolínea eliminada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar la aerolínea.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar la aerolínea.",
                    motivo = ex.Message
                });
            }
        }
    }
}