using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class TransaccionService
    {
        private readonly MilagrofinancieroG1Context _context;

        public TransaccionService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransaccionDtoOut>> GetAll()
        {
            return await _context.Transaccion
                .Select(t => new TransaccionDtoOut
                {
                    Monto = t.Monto,
                    NumeroOperacion = t.NumeroOperacion,
                    Acreditacion = t.Acreditacion,
                    Realizacion = t.Realizacion,
                    Motivo = t.Motivo,
                    Referencia = t.Referencia,
                    CuentaDestino = t.CuentaDestino.NumeroCuenta,
                    CuentaOrigen = t.CuentaOrigen.NumeroCuenta,
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
                    NumeroOperacion = t.NumeroOperacion,
                    Acreditacion = t.Acreditacion,
                    Realizacion = t.Realizacion,
                    Motivo = t.Motivo,
                    Referencia = t.Referencia,
                    CuentaDestino = t.CuentaDestino.NumeroCuenta,
                    CuentaOrigen = t.CuentaOrigen.NumeroCuenta,
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
            newTransaccion.Monto = newTransaccionDTO.Monto;
            newTransaccion.CuentaOrigenId = newTransaccionDTO.CuentaOrigenId;
            newTransaccion.CuentaDestinoId = newTransaccionDTO.CuentaDestinoId;
            newTransaccion.TipoTransaccionId = newTransaccionDTO.TipoTransaccionId;


            _context.Transaccion.Add(newTransaccion);
            await _context.SaveChangesAsync();

            return newTransaccion;
        }

        public async Task Update(int id, TransaccionDtoIn transaccion)
        {
            var existingTransaccion = await GetById(transaccion.Id);

            if (existingTransaccion is not null)
            {
                existingTransaccion.Monto = transaccion.Monto;
                existingTransaccion.Acreditacion = transaccion.Acreditacion;
                existingTransaccion.Realizacion = transaccion.Realizacion;
                existingTransaccion.Motivo = transaccion.Motivo;
                existingTransaccion.Referencia = transaccion.Referencia;
                existingTransaccion.CuentaOrigenId = transaccion.CuentaOrigenId;
                existingTransaccion.CuentaDestinoId = transaccion.CuentaDestinoId;
                existingTransaccion.TipoTransaccionId = transaccion.TipoTransaccionId;

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
    }
}
