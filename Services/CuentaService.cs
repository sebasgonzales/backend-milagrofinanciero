using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services.impl;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly MilagrofinancieroG1Context _context;

        public CuentaService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaDtoOut>> GetAll()
        {
            return await _context.Cuenta
                //Include(c => c.Banco)
                .Select(c => new CuentaDtoOut
                {
                    NumeroCuenta = c.NumeroCuenta,
                    Cbu = c.Cbu,
                    TipoCuenta = c.TipoCuenta.Nombre,
                    Banco = c.Banco.Nombre
                }).ToListAsync();
                

        }

        public Task<CuentaDtoOut> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
