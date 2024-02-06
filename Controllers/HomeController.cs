//HomeController
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // GET /Home/{username}/Cuentas
        [HttpGet("/Home/Cuentas/{username}")]
        public async Task<IActionResult> GetCuentasByClienteUsername(string username)
        {
            try
            {
                var cuentasCliente = await _homeService.GetCuentasByClienteUsername(username);
                return Ok(cuentasCliente);
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver respuesta adecuada.
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
