// HomeService
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class HomeService : IHomeService
    {
        private readonly milagrofinancierog1Context _context;

        public HomeService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteCuentaDtoOut>> GetCuentasByClienteUsername(string username)
        {
            var cuentasCliente = await _context.ClienteCuenta
                .Include(cc => cc.Cliente)
                .Include(cc => cc.Cuenta)
                .Where(cc => cc.Cliente.Username == username)
                .Select(cc => new ClienteCuentaDtoOut
                {
                    Titular = cc.Titular,
                    Alta = cc.Alta,
                    Cliente = cc.Cliente.Username,
                    Cuenta = cc.Cuenta.Numero
                }).ToListAsync();

            return cuentasCliente;
        }
    }
}
