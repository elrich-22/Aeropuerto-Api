using AeropuertoAPI.DTOs;
using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SesionUsuarioController : ControllerBase
    {
        private readonly SesionUsuarioService _service;

        public SesionUsuarioController(SesionUsuarioService service)
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
                    mensaje = "Error de Oracle al obtener las sesiones de usuario.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener las sesiones de usuario.",
                    motivo = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var sesion = await _service.ObtenerPorIdAsync(id);

                if (sesion == null)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la sesión de usuario."
                    });

                return Ok(sesion);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al obtener la sesión de usuario.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al obtener la sesión de usuario.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] SesionUsuarioCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CompletarDatosCliente(dto);
                var ok = await _service.CrearAsync(dto);

                if (!ok)
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear la sesión de usuario."
                    });

                return Ok(new
                {
                    mensaje = "Sesión de usuario creada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al crear la sesión de usuario.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al crear la sesión de usuario.",
                    motivo = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] SesionUsuarioUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CompletarDatosCliente(dto);
                var ok = await _service.ActualizarAsync(id, dto);

                if (!ok)
                    return NotFound(new
                    {
                        mensaje = "No se encontró la sesión de usuario a actualizar."
                    });

                return Ok(new
                {
                    mensaje = "Sesión de usuario actualizada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al actualizar la sesión de usuario.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al actualizar la sesión de usuario.",
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
                        mensaje = "No se encontró la sesión de usuario a eliminar."
                    });

                return Ok(new
                {
                    mensaje = "Sesión de usuario eliminada correctamente."
                });
            }
            catch (OracleException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error de Oracle al eliminar la sesión de usuario.",
                    oracleCode = ex.Number,
                    motivo = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error general al eliminar la sesión de usuario.",
                    motivo = ex.Message
                });
            }
        }

        private void CompletarDatosCliente(SesionUsuarioCreateDto dto)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();

            dto.IpAddress = ObtenerIpCliente();
            dto.Navegador = DetectarNavegador(userAgent);
            dto.SistemaOperativo = DetectarSistemaOperativo(userAgent);
            dto.Dispositivo = DetectarDispositivo(userAgent);
        }

        private void CompletarDatosCliente(SesionUsuarioUpdateDto dto)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();

            dto.IpAddress = ObtenerIpCliente();
            dto.Navegador = DetectarNavegador(userAgent);
            dto.SistemaOperativo = DetectarSistemaOperativo(userAgent);
            dto.Dispositivo = DetectarDispositivo(userAgent);
        }

        private string? ObtenerIpCliente()
        {
            var forwardedFor = Request.Headers["X-Forwarded-For"].ToString();

            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var ip = HttpContext.Connection.RemoteIpAddress;
            if (ip == null)
                return null;

            if (IPAddress.IsLoopback(ip))
                return "127.0.0.1";

            return ip.ToString();
        }

        private static string DetectarNavegador(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return "Desconocido";

            if (userAgent.Contains("Edg/", StringComparison.OrdinalIgnoreCase))
                return "Edge";

            if (userAgent.Contains("OPR/", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Opera", StringComparison.OrdinalIgnoreCase))
                return "Opera";

            if (userAgent.Contains("Chrome/", StringComparison.OrdinalIgnoreCase))
                return "Chrome";

            if (userAgent.Contains("Firefox/", StringComparison.OrdinalIgnoreCase))
                return "Firefox";

            if (userAgent.Contains("Safari/", StringComparison.OrdinalIgnoreCase) && !userAgent.Contains("Chrome/", StringComparison.OrdinalIgnoreCase))
                return "Safari";

            return "Desconocido";
        }

        private static string DetectarSistemaOperativo(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return "Desconocido";

            if (userAgent.Contains("Windows NT", StringComparison.OrdinalIgnoreCase))
                return "Windows";

            if (userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase))
                return "Android";

            if (userAgent.Contains("iPhone", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("iPad", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("iOS", StringComparison.OrdinalIgnoreCase))
                return "iOS";

            if (userAgent.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Macintosh", StringComparison.OrdinalIgnoreCase))
                return "macOS";

            if (userAgent.Contains("Linux", StringComparison.OrdinalIgnoreCase))
                return "Linux";

            return "Desconocido";
        }

        private static string DetectarDispositivo(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return "Desconocido";

            if (userAgent.Contains("Mobile", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("iPhone", StringComparison.OrdinalIgnoreCase))
                return "Mobile";

            if (userAgent.Contains("iPad", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Tablet", StringComparison.OrdinalIgnoreCase))
                return "Tablet";

            return "Desktop";
        }
    }
}
