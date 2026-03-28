using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VueloController : ControllerBase
    {
        private readonly VueloService _vueloService;

        public VueloController(VueloService vueloService)
        {
            _vueloService = vueloService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var vuelos = await _vueloService.ObtenerTodosAsync();
                return Ok(vuelos);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener los vuelos.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener los vuelos.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var vuelo = await _vueloService.ObtenerPorIdAsync(id);

                if (vuelo == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el vuelo."
                    });

                return Ok(vuelo);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener el vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener el vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VueloCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _vueloService.CrearAsync(dto);

                if (!creado)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear el vuelo."
                    });

                return Ok(new
                {
                    mensaje = "Vuelo creado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear el vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear el vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] VueloUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _vueloService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el vuelo a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Vuelo actualizado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar el vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar el vuelo.",
                    motivo = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _vueloService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new
                    {
                        mensaje = "No se encontró el vuelo a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Vuelo eliminado correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar el vuelo.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar el vuelo.",
                    motivo = ex.Message
                });
            }
        }
    }
}