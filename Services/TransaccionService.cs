using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
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

        public async Task<IEnumerable<Transaccion>> GetAll()
        {
            return await _context.Transaccions.ToListAsync();
        }

        public async Task<Transaccion?> GetById(int id)
        {
            return await _context.Transaccions.FindAsync(id);
        }

        public async Task<Transaccion> Create(Transaccion newTransaccion)
        {
            _context.Transaccions.Add(newTransaccion);
            await _context.SaveChangesAsync();

            return newTransaccion;
        }

        public async Task Update(int id, Transaccion transaccion)
        {
            var existingTransaccion = await GetById(transaccion.Id);

            if (existingTransaccion is not null)
            {
                existingTransaccion.Monto = transaccion.Monto;
                existingTransaccion.NumeroOperacion = transaccion.NumeroOperacion;
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
            _context.Transaccions.Remove(transaccionToDelete);
            await _context.SaveChangesAsync();
            }
        }
    }
}
