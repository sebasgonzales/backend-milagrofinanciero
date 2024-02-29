using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;



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
                    CbuDestino = t.CuentaDestino.Cbu,
                    CbuOrigen = t.CuentaOrigen.Cbu,
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
                    CbuDestino = t.CuentaDestino.Cbu,
                    CbuOrigen = t.CuentaOrigen.Cbu,
                    TipoTransaccion = t.TipoTransaccion.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Transaccion?> GetById(int id)
        {
            return await _context.Transaccion.FindAsync(id);
        }

        public async Task<Transaccion> CreateTransaccionInterna(TransaccionDtoIn newTransaccionDTO, int idCuentaDestino)
        {
            try
            {
                // Obtener el CBU de la cuenta destino por su ID
                string cbuDestino = await _cuentaService.GetCbuById(idCuentaDestino);

                // Verificar si los primeros 10 caracteres del CBU son "0000000001"
                if (cbuDestino.Substring(0, 10) == "0000000001")
                {
                    // Crear una nueva instancia de Transaccion y asignar los valores del DTO
                    var newTransaccion = new Transaccion
                    {
                        Monto = newTransaccionDTO.Monto,
                        Realizacion = newTransaccionDTO.Realizacion,
                        IdTipoMotivo = newTransaccionDTO.idTipoMotivo,
                        Referencia = newTransaccionDTO.Referencia,
                        IdTipoTransaccion = newTransaccionDTO.IdTipoTransaccion
                    };

                    // Obtener la cuenta de origen y destino a partir de sus IDs
                    Cuenta cuentaOrigen = await _context.Cuenta
                        .Where(c => c.Id == newTransaccionDTO.IdCuentaOrigen)
                        .FirstOrDefaultAsync();

                    Cuenta cuentaDestino = await _context.Cuenta
                        .Where(c => c.Id == newTransaccionDTO.IdCuentaDestino)
                        .FirstOrDefaultAsync();

                    if (cuentaDestino != null && cuentaOrigen != null)
                    {
                        // Configurar la cuenta de origen y destino, y guardar los cambios
                        newTransaccion.IdCuentaOrigen = cuentaOrigen.Id;
                        newTransaccion.IdCuentaDestino = cuentaDestino.Id;
                        _context.Transaccion.Add(newTransaccion);
                        await _context.SaveChangesAsync();

                        return newTransaccion;
                    }
                    else
                    {
                        // Manejar el caso en que la cuenta de destino o la cuenta origen no existe
                        throw new Exception("La cuenta de destino o la cuenta origen no existe.");
                    }
                }
                else if (cbuDestino.Substring(0, 10) == "0000000002" || cbuDestino.Substring(0, 10) == "0000000003")
                {
                    // Si el CBU corresponde a otro banco, verificar si la cuenta destino ya existe
                    Cuenta cuentaDestino = await _context.Cuenta
                        .Where(c => c.Cbu == cbuDestino)
                        .FirstOrDefaultAsync();

                    CuentaIdDtoOut cuentaDestinoIdDto = await _cuentaService.GetIdByCbu(cbuDestino);

                    /*if (cuentaDestinoIdDto == null)
                    {
                        // Si la cuenta de destino no existe, crear una cuenta externa para ese banco
                        cuentaDestino = await _cuentaService.CreateCuentaExterna(cbuDestino);
                        cuentaDestinoIdDto = new CuentaIdDtoOut { Id = cuentaDestino.Id };
                    }*/

                    // Crear la transacción con la cuenta de origen y la cuenta destino externa
                    var newTransaccion = new Transaccion
                    {
                        Monto = newTransaccionDTO.Monto,
                        Realizacion = newTransaccionDTO.Realizacion,
                        IdTipoMotivo = newTransaccionDTO.idTipoMotivo,
                        Referencia = newTransaccionDTO.Referencia,
                        IdTipoTransaccion = newTransaccionDTO.IdTipoTransaccion,
                        IdCuentaOrigen = newTransaccionDTO.IdCuentaOrigen,
                        IdCuentaDestino = cuentaDestinoIdDto.Id
                    };

                    // Guardar la transacción en la base de datos
                    _context.Transaccion.Add(newTransaccion);
                    await _context.SaveChangesAsync();

                    return newTransaccion;
                }
                else
                {
                    // Manejar el caso en que los primeros 10 caracteres del CBU no son "0000000001", "0000000002" ni "0000000003"
                    throw new Exception("El CBU de la cuenta destino no cumple con el formato requerido.");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception("Error al crear la transacción", ex);
            }
        }

        




        //TRANSACCION ORIGEN EXTENRO
        public async Task<Transaccion> CreateTransaccionExterna(TransaccionExternaDtoIn newTransaccionExternaDTO)
        {
            try
            {
                // Verificar si el monto es mayor que cero
                if (newTransaccionExternaDTO.Monto <= 0)
                {
                    throw new ArgumentException("El monto de la transacción debe ser mayor que cero.");
                }

                // Obtener el ID de la cuenta de origen a partir del CBU proporcionado
                CuentaIdDtoOut cuentaOrigenIdDto = await _cuentaService.GetIdByCbu(newTransaccionExternaDTO.CbuCuentaOrigen);
                if (cuentaOrigenIdDto == null)
                {
                    // Si la cuenta de origen no existe, la creo
                   await _cuentaService.CreateCuentaExterna(newTransaccionExternaDTO.CbuCuentaOrigen);
                }

                // Obtener el ID de la cuenta de destino a partir del CBU proporcionado
                CuentaIdDtoOut cuentaDestinoIdDto = await _cuentaService.GetIdByCbu(newTransaccionExternaDTO.CbuCuentaDestino);
                if (cuentaDestinoIdDto == null)
                {
                    // Si la cuenta de destino no existe, lanzar una excepción o manejar el error según sea necesario
                    throw new Exception("La cuenta de destino no existe.");
                }
                // Crear una nueva instancia de Transaccion y asignar los valores del json externo recibido
                var newTransaccion = new Transaccion
                {
                    Monto = newTransaccionExternaDTO.Monto,
                    Realizacion = newTransaccionExternaDTO.Realizacion,
                    IdTipoMotivo = 1,
                    Referencia = "varios",
                    IdTipoTransaccion = 2, //inmediata
                    IdCuentaOrigen = cuentaOrigenIdDto.Id,
                    IdCuentaDestino = cuentaDestinoIdDto.Id
                 };

                // Guardar la transacción en la base de datos
                _context.Transaccion.Add(newTransaccion);
                await _context.SaveChangesAsync();

                return newTransaccion;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception("Error al crear la transacción", ex);
            }
        }


        


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
                    CbuDestino = t.CuentaDestino.Cbu,
                    CbuOrigen = t.CuentaOrigen.Cbu,
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
}