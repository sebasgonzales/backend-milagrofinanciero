using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;
using GeneradorNumeros;
using System.Diagnostics;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using static System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Services
{
    public class CuentaService : ICuentaService
    {
        private readonly milagrofinancierog1Context _context;


        public CuentaService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaDtoOut>> GetAll()
        {
            return await _context.Cuenta
                //Include(c =>
                //Banco)
                .Select(c => new CuentaDtoOut
                {
                    NumeroCuenta = c.Numero,
                    Cbu = c.Cbu,
                    TipoCuenta = c.TipoCuenta.Nombre,
                    Banco = c.Banco.Nombre,
                    Sucursal = c.Sucursal.Nombre
                }).ToListAsync();


        }

        public async Task<CuentaDtoOut?> GetByIdDto(int id)
        {
            return await _context.Cuenta
                .Where(c => c.Id == id)
                .Select(c => new CuentaDtoOut
                {
                    NumeroCuenta = c.Numero,
                    Cbu = c.Cbu,
                    TipoCuenta = c.TipoCuenta.Nombre,
                    Banco = c.Banco.Nombre,
                    Sucursal = c.Sucursal.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Cuenta?> GetById(int id)
        {
            return await _context.Cuenta
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Cuenta> Create(CuentaDtoIn newCuentaDto)
        {
            var newCuenta = new Cuenta();
            int numFijo = 111;
            long numAleatorio = Array.ConvertAll(AlgoritmoGenerador.GenerarNumerosAleatorios(), x => (int)x)[0];
            long numCuenta = long.Parse(numFijo.ToString() + numAleatorio.ToString());

            // Obtengo el código del banco (string)
            var codigoBanco = await _context.Banco
                .Where(b => b.Id == newCuentaDto.IdBanco)
                .Select(b => b.Codigo)
                .FirstOrDefaultAsync();
            if (codigoBanco == null)
            {
                throw new Exception("No se encontro el código del banco.");
            }
            string cbu = codigoBanco + numCuenta.ToString();

            newCuenta.Numero = numCuenta;
            newCuenta.Cbu = cbu;
            newCuenta.IdTipoCuenta = newCuentaDto.IdTipoCuenta;
            newCuenta.IdBanco = newCuentaDto.IdBanco;
            newCuenta.IdSucursal = newCuentaDto.IdSucursal;



            _context.Cuenta.Add(newCuenta);
            await _context.SaveChangesAsync();

            return newCuenta;
        }

        public async Task Update(int id, CuentaDtoIn cuenta)
        {
            var cuentaExistente = await GetById(id);
            if (cuentaExistente is not null)
            {
                cuentaExistente.Numero = cuenta.Numero;
                cuentaExistente.Cbu = cuenta.Cbu;
                cuentaExistente.IdTipoCuenta = cuenta.IdTipoCuenta;
                cuentaExistente.IdBanco = cuenta.IdBanco;
                cuentaExistente.IdSucursal = cuenta.IdSucursal;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var cuentaParaEliminar = await GetById(id);
            if (cuentaParaEliminar is not null)
            {
                _context.Cuenta.Remove(cuentaParaEliminar);
                await _context.SaveChangesAsync();
            }
        }

        /*
         public async Task<Cuenta> GetCuentaByCbu(long cbu)
        {
            return await _context.Cuenta
                .FirstOrDefaultAsync(c => c.Cbu == cbu);
        }
        public async Task<Cuenta> GetCuentaByNumero(long numeroCuenta)
        {
            return await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .FirstOrDefaultAsync();
        }
        
         */

        public async Task<CuentaIdDtoOut> GetIdByCbu(string cbu)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Cbu == cbu)
                .Select(c => new CuentaIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return cuenta;
        }

        public async Task<CuentaIdDtoOut?> GetIdByNumeroCuenta(long numeroCuenta)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => new CuentaIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return cuenta;
        }

        public async Task<List<ContactoDtoOut>> GetContactos(long numeroCuenta)
        {
            //obtengo el id de la cuenta
            var cuentaId = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); //devuelve si encontro o sino default
            if (cuentaId == default)
            {
                return new List<ContactoDtoOut>(); // No esta el cliente, devuelve lista vacia
            }
            //si se encontró los contactos
            var contactos = await _context.Contacto
                .Where(cc => cc.IdCuenta == cuentaId) // cruzo las tablas
                .Select(cc => new ContactoDtoOut
                {

                    Nombre = cc.Nombre,
                    Cbu = cc.Cbu,
                    Banco = cc.Banco.Nombre
                }).ToListAsync();

            return contactos;

        }

        //obtener cbu por numero de cuenta
        public async Task<string?> GetCbuByNumeroCuenta(long numeroCuenta)
        {
            var cbu = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => c.Cbu)
                .SingleOrDefaultAsync();

            return cbu;
        }



        public async Task<string?> AutenticacionSRVP(string authorizationCode)
        {
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
            RestResponse response = await client.ExecuteAsync(request);

            Console.WriteLine("El response.Content de tipo RestResponse: " + response.Content + "\n");

            // JWT
            var clienteRespuestaJWT = System.Text.Json.JsonSerializer.Deserialize<ClienteRenaperDtoOut>(response.Content);
            Console.WriteLine("El response.Content deserealizado json: " + clienteRespuestaJWT + "\n");

            var datosEncriptados = clienteRespuestaJWT.datos;
            Console.WriteLine("Datos encriptados, esto debería ser un JWT: " + datosEncriptados + "\n");

            // Clave pública proporcionada
            string modulus = "9BJ0WxXATSJ6KtiSHhglSd3kgc6j5kXLp8sx5hm5KN2Y8H1uygVrPAJGBqPEIgRpMHG8yMFyKh2hXLSnZNLtZ+7c+fMIUYJYARS8f4yxF3CpkMtVW4wJ5Sbg99vIyi8Hi/134QuwU9ghYKiGgaYEvsQo5P9R+y/MiJrclETu5mkUdazs0Sua5+WdnsmJqykVxrfHtgvlavtmhF2B8zUWWOb8zdPgWqzxULt4RHWIasdf6GxzG+XGK+6jyNfb4DpUJQBlHssVGgflNEukoYefTcqx865JeGMeIBJzmxceiD2PrEnDsHHYk8w5/2dAWbnf8Pk19T3CXDDd73MLiPR5xQ==";
            string exponent = "AQAB";

            // Verificar firma
            bool firmaValida = await VerificarFirmaJWTAsync(clienteRespuestaJWT, modulus, exponent);
            Console.WriteLine("La firma del JWT es válida: " + firmaValida);

            // Llama a la función para decodificar el payload Base64
            string[] jwtParts = datosEncriptados.Split('.');

            var payloadObject = DecodeBase64JsonPayload(datosEncriptados);
            if (firmaValida == true)
            {
                return payloadObject;
            }
            else
            {
                return null;
            }
        }

        // Función para decodificar el payload Base64 y devolverlo como cadena JSON
        private string DecodeBase64JsonPayload(string base64Payload)
        {
            try
            {
                string[] jwtParts = base64Payload.Split('.');
                if (jwtParts.Length >= 2)
                {
                    string payload = jwtParts[1];
                    Console.WriteLine("Soy el payload: " + payload + "\n");
                    // Validar la cadena Base64 antes de decodificar
                    if (IsBase64String(payload))
                    {
                        // Decodificar el payload Base64
                        byte[] decodedPayload = Convert.FromBase64String(payload);
                        string decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);

                        Console.WriteLine("Payload decodificado: " + decodedPayloadString);
                        return decodedPayloadString;
                    }
                    else
                    {
                        Console.WriteLine("La cadena Base64 del Payload no es válida.");
                        return string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("El JWT no tiene suficientes partes.");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al decodificar el payload Base64: " + ex.Message);
                return string.Empty;
            }
        }

        private bool IsBase64String(string s)
        {
            try
            {
                Convert.FromBase64String(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Función para validar la firma
        private async Task<bool> VerificarFirmaJWTAsync(ClienteRenaperDtoOut clienteRespuestaJWT, string modulus, string exponent)
        {
            try
            {
                // Parsear el JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(clienteRespuestaJWT.datos);

                // Obtener la parte de la firma del JWT
                string firmaBase64 = jwtToken.RawSignature;

                Console.WriteLine("Firma Base64: " + firmaBase64);

                // Decodificar la firma Base64
                byte[] firma = Convert.FromBase64String(firmaBase64);

                // Construir la clave pública RSA
                RSAParameters rsaParams = new RSAParameters
                {
                    Modulus = Convert.FromBase64String(modulus),
                    Exponent = Convert.FromBase64String(exponent)
                };

                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportParameters(rsaParams);

                    // Verificar la firma
                    bool firmaVerificada = await Task.Run(() =>
                        rsa.VerifyData(Encoding.UTF8.GetBytes(jwtToken.EncodedHeader + "." + jwtToken.EncodedPayload), firma, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));

                    Console.WriteLine("Firma verificada: " + firmaVerificada);

                    return firmaVerificada;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar la firma del JWT: " + ex.Message);
                return false;
            }
        }

    }
}