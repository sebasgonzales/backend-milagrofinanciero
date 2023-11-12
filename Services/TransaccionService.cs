using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_milagrofinanciero.Services
{
    public class TransaccionService
    {
        private readonly MilagrofinancieroG1Context _context;

        public TransaccionService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        public IEnumerable<Transaccion> GetAll()
        {
            return _context.Transaccions.ToList();
        }

        public Transaccion? GetById(int id)
        {
            return _context.Transaccions.Find(id);
        }

        public Transaccion Create(Transaccion newTransaccion)
        {
            _context.Transaccions.Add(newTransaccion);
            _context.SaveChanges();

            return newTransaccion;
        }

        public void Update(int id, Transaccion transaccion)
        {
            var existingTransaccion = GetById(transaccion.Id);

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

                _context.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            var transaccionToDelete = GetById(id);
            if (transaccionToDelete is not null)
            { 
            _context.Transaccions.Remove(transaccionToDelete);
            _context.SaveChanges();
            }
        }
    }
}
