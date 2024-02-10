using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService) {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Llamar al servicio de autenticación con el usuario y contraseña proporcionados
            var cuitCuil = await _loginService.AuthenticateClientAndGetCuitCuil(request.Username, request.Password);

            // Si el cliente no pudo ser autenticado, devolver un error
            if (cuitCuil == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Si el cliente fue autenticado correctamente, devolver su CuitCuil
            return Ok(cuitCuil);
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
