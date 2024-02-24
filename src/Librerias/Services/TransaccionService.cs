using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using RestSharp.Authenticators;



namespace Services
{
    public class TransaccionService : ITransaccionService
    {
        private readonly milagrofinancierog1Context _context;
        private readonly ICuentaService _cuentaService;
        public TransaccionService(milagrofinancierog1Context context, ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
            _context = context;
        }

        public async Task<IEnumerable<TransaccionDtoOut>> GetAll()
        {
            return await _context.Transaccion
                .Select(t => new TransaccionDtoOut
                {
                    Monto = t.Monto,
                    Numero = t.Numero,
                    Realizacion = t.Realizacion,
                    Motivo = t.TipoMotivo.Nombre,
                    Referencia = t.Referencia,
                    CuentaDestino = t.CuentaDestino.Numero,
                    CuentaOrigen = t.CuentaOrigen.Numero,
                    TipoTransaccion = t.TipoTransaccion.Nombre

                }).ToListAsync();
        }

        public async Task<TransaccionDtoOut?> GetDtoById(int id)
        {
            return await _context.Transaccion
                .Where(t => t.Id == id)
                .Select(t => new TransaccionDtoOut
                {
                    Monto = t.Monto,
                    Numero = t.Numero,
                    Realizacion = t.Realizacion,
                    Motivo = t.TipoMotivo.Nombre,
                    Referencia = t.Referencia,
                    CuentaDestino = t.CuentaDestino.Numero,
                    CuentaOrigen = t.CuentaOrigen.Numero,
                    TipoTransaccion = t.TipoTransaccion.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Transaccion?> GetById(int id)
        {
            return await _context.Transaccion.FindAsync(id);
        }

        public async Task<Transaccion> Create(TransaccionDtoIn newTransaccionDTO)
        {
            try
            {
                // Crear una nueva instancia de Transaccion y asignar los valores del DTO
                var newTransaccion = new Transaccion
                {
                    Monto = newTransaccionDTO.Monto,
                    Realizacion = newTransaccionDTO.Realizacion,
                    IdTipoMotivo = newTransaccionDTO.idTipoMotivo,
                    Referencia = newTransaccionDTO.Referencia,
                    //IdCuentaOrigen = newTransaccionDTO.IdCuentaOrigen,
                    //IdCuentaDestino = newTransaccionDTO.IdCuentaDestino,
                    IdTipoTransaccion = newTransaccionDTO.IdTipoTransaccion
                };

                // Obtener la cuenta de destino a partir del CBU
                Cuenta cuentaDestino = await _context.Cuenta
                    .Where(c => c.Id == newTransaccionDTO.IdCuentaDestino)
                    .FirstOrDefaultAsync();

                Cuenta cuentaOrigen = await _context.Cuenta
                    .Where(c => c.Id == newTransaccionDTO.IdCuentaOrigen)
                    .FirstOrDefaultAsync();

                //CuentaIdDtoOut cuentaOrigenDto = await GetCuenIdDtoByNumeroCuenta(numeroCuentaOrigen);
                if (cuentaDestino != null && cuentaOrigen != null)
                {
                    // Configurar la cuenta de destino y guardar los cambios
                    newTransaccion.IdCuentaOrigen = cuentaOrigen.Id;
                    newTransaccion.IdCuentaDestino = cuentaDestino.Id;
                    _context.Transaccion.Add(newTransaccion);
                    await _context.SaveChangesAsync();

                    return newTransaccion;
                }
                else
                {
                    // Manejar el caso en que la cuenta de destino no existe
                    // Puedes lanzar una excepci�n, devolver un c�digo de error, etc.
                    throw new Exception("La cuenta de destino o la cuenta origen no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepci�n seg�n tus necesidades
                throw new Exception("error", ex);
            }
        }

        //NO SE DEBERIA ACTUALIZAR UNA TRANSACCION
        //public async Task Update(TransaccionDtoIn transaccion)
        //{
        //    var existingTransaccion = await GetById(transaccion.Id);

        //    if (existingTransaccion is not null)
        //    {
        //        existingTransaccion.Monto = transaccion.Monto;
        //        existingTransaccion.Realizacion = transaccion.Realizacion;
        //        existingTransaccion.Motivo = transaccion.Motivo;
        //        existingTransaccion.Referencia = transaccion.Referencia;
        //        existingTransaccion.IdCuentaOrigen = transaccion.IdCuentaOrigen;
        //        existingTransaccion.IdCuentaDestino = transaccion.IdCuentaDestino;
        //        existingTransaccion.IdTipoTransaccion = transaccion.IdTipoTransaccion;

        //        await _context.SaveChangesAsync();
        //    }

        //}

        //NO SE DEBERIA ELIMINAR UNA TRANSACCION

        //public async Task Delete(int id)
        //{
        //    var transaccionToDelete = await GetById(id);
        //    if (transaccionToDelete is not null)
        //    {
        //        _context.Transaccion.Remove(transaccionToDelete);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta)
        {
            return await _context.Transaccion
                .Where(t => t.CuentaOrigen.Numero == numeroCuenta || t.CuentaDestino.Numero == numeroCuenta)
                .Select(t => new TransaccionDtoOut
                {
                    Monto = t.Monto,
                    Numero = t.Numero,
                    Realizacion = t.Realizacion,
                    Motivo = t.TipoMotivo.Nombre,
                    Referencia = t.Referencia,
                    CuentaDestino = t.CuentaDestino.Numero,
                    CuentaOrigen = t.CuentaOrigen.Numero,
                    TipoTransaccion = t.TipoTransaccion.Nombre
                }).ToListAsync();
        }

        public async Task<int> VerificadorSaldo(long numeroCuenta, float monto)
        {
            int id = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            float saldo = await _context.Transaccion
                .Where(t => t.IdCuentaDestino == id)
                .SumAsync(t => t.Monto);
            if (saldo >= monto)
            {
                return 1;
            }
            else
            {
                return 0;

            }

        }

        //PARA SABER EL SALDO DE MI CUENTA sumando todas las transacciones existentes
        public async Task<float> ObtenerSaldo(long numeroCuenta)
        {

            var options = new RestClientOptions("https://api.twitter.com/1.1")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("statuses/home_timeline.json");
            // The cancellation token comes from the caller. You can still make a call without it.
            var response = await client.GetAsync<Rootobject>(request);

            // Obtener la cuenta de origen
            var cuenta = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .FirstOrDefaultAsync();

            if (cuenta != null)
            {
                // Calcular el saldo restando los montos de las transacciones salientes y sumando las entrantes
                float saldo = await _context.Transaccion
                    .Where(t => t.IdCuentaOrigen == cuenta.Id)
                    .SumAsync(t => -t.Monto) + await _context.Transaccion
                    .Where(t => t.IdCuentaDestino == cuenta.Id)
                    .SumAsync(t => t.Monto);

                return saldo;
            }
            else
            {
                // Manejar el caso en que la cuenta no existe
                return -1; // o alg�n valor que indique que la cuenta no fue encontrada

            }
        }
    }


    public class Rootobject
    {
        public int id { get; set; }
        public int monto { get; set; }
        public DateTime realizacion { get; set; }
        public int idTipoMotivo { get; set; }
        public string referencia { get; set; }
        public int idCuentaOrigen { get; set; }
        public int idCuentaDestino { get; set; }
        public int idTipoTransaccion { get; set; }
    }

}