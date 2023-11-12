using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class TipoCuentaService
    {

        private readonly MilagrofinancieroG1Context _context;
        public TipoCuentaService(MilagrofinancieroG1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoCuenta>> GetAll()
        {
            return await _context.TipoCuenta.ToListAsync();

        }

        public async Task<TipoCuenta?> GetById(int id)
        {
            return await _context.TipoCuenta.FindAsync(id);
        }

        public async Task<TipoCuenta> Create(TipoCuenta newtipoCuenta)
        {
            _context.TipoCuenta.Add(newtipoCuenta);
            await _context.SaveChangesAsync();

            return newtipoCuenta;

        }

        public async Task Update(int id, TipoCuenta tipoCuenta)
        {
            var existingtipoCuenta = await GetById(id);

            if (existingtipoCuenta is not null)
            {

                existingtipoCuenta.Nombre = tipoCuenta.Nombre;
                existingtipoCuenta.Alta = tipoCuenta.Alta;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var tipoCuentaToDelete = await GetById(id);

            if (tipoCuentaToDelete is not null)
            {

                _context.TipoCuenta.Remove(tipoCuentaToDelete);
                await _context.SaveChangesAsync();
            }

        }


    }
}