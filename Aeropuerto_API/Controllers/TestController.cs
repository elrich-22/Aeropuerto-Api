using AeropuertoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeropuertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly TestConnectionService _testConnectionService;

        public TestController(TestConnectionService testConnectionService)
        {
            _testConnectionService = testConnectionService;
        }

        [HttpGet("conexion")]
        public async Task<IActionResult> ProbarConexion()
        {
            var resultado = await _testConnectionService.ProbarConexionAsync();
            return Ok(resultado);
        }
    }
}