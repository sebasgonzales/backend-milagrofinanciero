using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Services
{
    public class ClienteService : IClienteService
    {
        private readonly milagrofinancierog1Context _context;
        private readonly Hashear _hashing;
        public ClienteService(milagrofinancierog1Context context, Hashear hashing)
        {
            _context = context;
            _hashing = hashing;
        }

        public async Task<IEnumerable<ClienteDtoOut>> GetAll()
        {
            return await _context.Cliente
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).ToListAsync();
        }

        // GetById con Dto
        public async Task<ClienteDtoOut?> GetByIdDto(int id)
        {
            return await _context.Cliente
                .Where(c => c.Id == id)
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).SingleOrDefaultAsync();
        }


        // GetNombre nuevo
        public async Task<string> GetNombre(string cuitCuil)
        {
            var cliente = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).SingleOrDefaultAsync();
            return cliente.Nombre;
        }


        // GetById sin Dto
        public async Task<Cliente?> GetById(int id)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();
            return cliente;
        }


        public async Task<Cliente> Create(ClienteDtoIn newClienteDTO)
        {
            var newCliente = new Cliente();

            newCliente.Nombre = newClienteDTO.Nombre;
            newCliente.Apellido = newClienteDTO.Apellido;
            newCliente.Alta = newClienteDTO.Alta;
            newCliente.CuitCuil = newClienteDTO.CuitCuil;
            newCliente.Calle = newClienteDTO.Calle;
            newCliente.Departamento = newClienteDTO.Departamento;
            newCliente.AlturaCalle = newClienteDTO.AlturaCalle;
            newCliente.Username = newClienteDTO.Username;
            newCliente.Password = _hashing.HashearConSHA256(newClienteDTO.Password);
            newCliente.IdLocalidad = newClienteDTO.IdLocalidad;

            _context.Cliente.Add(newCliente);
            await _context.SaveChangesAsync();

            return newCliente;
        }
        public async Task Update(int id, ClienteDtoIn cliente)
        {

            var existingClient = await GetById(id);

            if (existingClient is not null)
            {
                existingClient.Nombre = cliente.Nombre;
                existingClient.Apellido = cliente.Apellido;
                existingClient.Alta = cliente.Alta;
                existingClient.CuitCuil = cliente.CuitCuil;
                existingClient.Calle = cliente.Calle;
                existingClient.Departamento = cliente.Departamento;
                existingClient.AlturaCalle = cliente.AlturaCalle;
                existingClient.Username = cliente.Username;
                existingClient.Password = cliente.Password;
                existingClient.IdLocalidad = existingClient.IdLocalidad;

                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {

            var clientToDelete = await GetById(id);

            if (clientToDelete is not null)
            {
                _context.Cliente.Remove(clientToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CuentaDtoOut>> GetCuentasByCuitCuil(string cuitCuil)
        {
            //obtengo el id del cliente
            var clienteId = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); //devuelve si encontro o sino default

            if (clienteId == default)
            {
                return new List<CuentaDtoOut>(); // No esta el cliente, devuelve lista vacia
            }

            //si se encontro el cliente
            var cuentas = await _context.ClienteCuenta
                .Where(cc => cc.IdCliente == clienteId) // cruzo las tablas
                .Select(cc => new CuentaDtoOut
                {
                    NumeroCuenta = cc.Cuenta.Numero,
                    Cbu = cc.Cuenta.Cbu,
                    TipoCuenta = cc.Cuenta.TipoCuenta.Nombre,
                    Banco = cc.Cuenta.Banco.Nombre,
                    Sucursal = cc.Cuenta.Sucursal.Nombre
                }).ToListAsync();

            return cuentas;
        }

        public async Task<List<ClienteDtoOut>> GetClienteByCuitCuil(string cuitCuil)
        {
            // Obtengo el id del cliente
            var clienteId = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); // Devuelve si encontró o sino default

            if (clienteId == default)
            {
                return new List<ClienteDtoOut>(); // No está el cliente, devuelve lista vacía
            }

            // Si se encontró el cliente, selecciono y mapeo sus datos
            var cliente = await _context.Cliente
                .Where(c => c.Id == clienteId)
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).ToListAsync();

            return cliente;
        }

        public async Task<ClienteIdDtoOut> GetIdByCuitCuil(string cuitCuil)
        {
            var clienteId = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => new ClienteIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return clienteId;
        }

        public async Task<ClienteRenaper> AutenticacionSRVP(string authorizationCode)
        {
            var respuesta = new ClienteRenaper();
            
                const string clientId = "a73b2d37-c304-431d-8817-26f3c5b2254a";
                const string clientSecret = "deamon16";

                var options = new RestClientOptions("https://colosal.duckdns.org:15001")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/renaper/api/Auth/loguearJWT", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body =
                @"{
                    " + "\n" +
                                @"    ""clientId"" : """ + clientId + @""",
                    " + "\n" +
                                @"    ""clientSecret"" : """ + clientSecret + @""",
                    " + "\n" +
                                @"    ""authorizationCode"" : """ + authorizationCode + @"""
                    " + "\n" +
                @"}";
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = await client.ExecuteAsync<string>(request);

                Console.WriteLine("El response.Content de tipo RestResponse: " + response.Content + "\n");

            // JWT
                var clienteRespuestaJWT = System.Text.Json.JsonSerializer.Deserialize<ClienteRenaperDtoOut>(response.Content);
                Console.WriteLine("El response.Content deserealizado json: " + clienteRespuestaJWT + "\n");

                var datosEncriptados = clienteRespuestaJWT.datos;
                Console.WriteLine("Datos encriptados, esto debería ser un JWT: " + datosEncriptados + "\n");
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Cargar la clave pública RSA desde el XML
                RSA rsaPublicKey = RSA.Create();
                rsaPublicKey.FromXmlString(@"<RSAKeyValue><Modulus>9BJ0WxXATSJ6KtiSHhglSd3kgc6j5kXLp8sx5hm5KN2Y8H1uygVrPAJGBqPEIgRpMHG8yMFyKh2hXLSnZNLtZ+7c+fMIUYJYARS8f4yxF3CpkMtVW4wJ5Sbg99vIyi8Hi/134QuwU9ghYKiGgaYEvsQo5P9R+y/MiJrclETu5mkUdazs0Sua5+WdnsmJqykVxrfHtgvlavtmhF2B8zUWWOb8zdPgWqzxULt4RHWIasdf6GxzG+XGK+6jyNfb4DpUJQBlHssVGgflNEukoYefTcqx865JeGMeIBJzmxceiD2PrEnDsHHYk8w5/2dAWbnf8Pk19T3CXDDd73MLiPR5xQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsaPublicKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(datosEncriptados, validationParameters, out SecurityToken validatedToken);

                var jwtPayload = ((JwtSecurityToken)validatedToken).Payload;

                var clienteRenaper = new ClienteRenaper
                {
                    Nombre = jwtPayload["Nombre"].ToString(),
                    Rol = jwtPayload["Rol"].ToString(),
                    Apellido = jwtPayload["Apellido"].ToString(),
                    Email = jwtPayload["Email"].ToString(),
                    Cuil = jwtPayload["Cuil"].ToString(),
                    Estado = Convert.ToBoolean(jwtPayload["Estado"]),
                    EstadoCrediticio = Convert.ToBoolean(jwtPayload["EstadoCrediticio"])
                };

                Console.WriteLine("Se decodificó el JWT con éxito :-)");

                //clienteRespuestaJWT.datos = datosEncriptados;
                clienteRespuestaJWT.exito = true;
                clienteRespuestaJWT.mensaje = "Cliente validado correctamente";
                clienteRespuestaJWT.Nombre = clienteRenaper.Nombre;
                clienteRespuestaJWT.Rol = clienteRenaper.Rol;
                clienteRespuestaJWT.Email = clienteRenaper.Email;
                clienteRespuestaJWT.Cuil = clienteRenaper.Cuil;
                clienteRespuestaJWT.Estado = clienteRenaper.Estado;
                clienteRespuestaJWT.EstadoCrediticio = clienteRenaper.EstadoCrediticio;
                Console.WriteLine("Datos: \n" +clienteRespuestaJWT);

            }
            catch (Exception e)
            { Console.WriteLine(e.ToString());
                return respuesta;
            }

            if (clienteRespuestaJWT.exito = false)
            {
                return respuesta;
            }
            else
            {
                return respuesta;
            }
        }

        // Asegurar que la cadena Base64 tenga la longitud adecuada
        static string PadBase64String(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: return base64 + "==";
                case 3: return base64 + "=";
                default: return base64;
            }
        }
        
    }

}

