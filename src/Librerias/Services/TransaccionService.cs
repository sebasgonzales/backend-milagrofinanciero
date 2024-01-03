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

        public TransaccionService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransaccionDtoOut>> GetAll()
        {
            return await _context.Transaccion
                .Select(t => new TransaccionDtoOut
                {
                    Monto = t.Monto,
                    Numero = t.Numero,
                    Acreditacion = t.Acreditacion,
                    Realizacion = t.Realizacion,
                    Motivo = t.Motivo,
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
                    Acreditacion = t.Acreditacion,
                    Realizacion = t.Realizacion,
                    Motivo = t.Motivo,
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
            var newTransaccion = new Transaccion();

            newTransaccion.Monto = newTransaccionDTO.Monto;
            //newTransaccion para Nro de op no, porque se autoincrementa
            newTransaccion.Acreditacion = newTransaccionDTO.Acreditacion;
            newTransaccion.Realizacion = newTransaccionDTO.Realizacion;
            newTransaccion.Motivo = newTransaccionDTO.Motivo;
            newTransaccion.Referencia = newTransaccionDTO.Referencia;
            //newTransaccion.Monto = newTransaccionDTO.Monto;
            newTransaccion.IdCuentaOrigen = newTransaccionDTO.IdCuentaOrigen;
            newTransaccion.IdCuentaDestino = newTransaccionDTO.IdCuentaDestino;
            newTransaccion.IdTipoTransaccion = newTransaccionDTO.IdTipoTransaccion;


            _context.Transaccion.Add(newTransaccion);
            await _context.SaveChangesAsync();

            return newTransaccion;
        }

        public async Task Update(TransaccionDtoIn transaccion)
        {
            var existingTransaccion = await GetById(transaccion.Id);

            if (existingTransaccion is not null)
            {
                existingTransaccion.Monto = transaccion.Monto;
                existingTransaccion.Acreditacion = transaccion.Acreditacion;
                existingTransaccion.Realizacion = transaccion.Realizacion;
                existingTransaccion.Motivo = transaccion.Motivo;
                existingTransaccion.Referencia = transaccion.Referencia;
                existingTransaccion.IdCuentaOrigen = transaccion.IdCuentaOrigen;
                existingTransaccion.IdCuentaDestino = transaccion.IdCuentaDestino;
                existingTransaccion.IdTipoTransaccion = transaccion.IdTipoTransaccion;

                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var transaccionToDelete = await GetById(id);
            if (transaccionToDelete is not null)
            {
                _context.Transaccion.Remove(transaccionToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetSaldo(long cbu, float monto)
        {
            int id = await _context.Cuenta
                .Where(c => c.Cbu == cbu)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            float saldo = await  _context.Transaccion
                .Where(t => t.IdCuentaDestino == id)
                .SumAsync(t => t.Monto);
            if (saldo > monto)
            {
                return 1;
            }
            else
            {
                return 0;

            }
        }
    }
}
