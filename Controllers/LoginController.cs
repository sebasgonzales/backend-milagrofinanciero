using Core.DTO.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;

        // Inyecta la configuración de tu app para generar el JWT
        private IConfiguration _config;

        public LoginController(ILoginService loginService, IConfiguration config) {
            _loginService = loginService;
            _config = config;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginRequest request) //saque el front body
        {
            // Llamar al servicio de autenticación con el usuario y contraseña proporcionados
            var cliente = await _loginService.AuthenticateCliente(request.Username, request.Password);

            // Si el cliente no pudo ser autenticado, devolver un error
            if (cliente == null)

                return BadRequest("Usuario o contraseña incorrectos");

            string clienteRespuestaJWT = GenerarToken(cliente);


            // Si el cliente fue autenticado correctamente, devolver su CuitCuil
            return Ok(new { token = clienteRespuestaJWT });
        }

        private string GenerarToken(ClienteDtoOut cliente)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, cliente.Nombre)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds
           );
            //serializa el token
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        [HttpPost("GetCuitCuil")]
        public async Task<IActionResult> ObtenerCuitCuilLogin([FromBody] LoginRequest request)
        {
            // Llamar al servicio de autenticación con el usuario y contraseña proporcionados
            var cuitCuil = await _loginService.LoginAndGetCuitCuil(request.Username, request.Password);

            // Si el cliente no pudo ser autenticado, devolver un error
            if (cuitCuil == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Si el cliente fue autenticado correctamente, devolver su CuitCuil
            return Ok(cuitCuil);
        }


    }

}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
