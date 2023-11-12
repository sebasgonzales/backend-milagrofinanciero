
using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class BancoService
    {

        private readonly MilagrofinancieroG1Context _context;
        public BancoService(MilagrofinancieroG1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Banco>>GetAll()
        {
            return await _context.Bancos.ToListAsync();

        }

        public async Task<Banco?>GetById(int id)
        {
            return await  _context.Bancos.FindAsync(id);
        }

        public async Task<Banco>Create(Banco newbanco)
        {
            _context.Bancos.Add(newbanco);
           await _context.SaveChangesAsync(); 

            return newbanco;

        }

        public async Task Update(int id, Banco banco)
        {
            var existingBanco = await GetById(id);

            if (existingBanco is not null)
            {
               
                existingBanco.Nombre = banco.Nombre;
               await  _context.SaveChangesAsync();
            }
          
        }

        public async Task Delete(int id)
        {
            var bancoToDelete = await GetById(id);

            if (bancoToDelete is not null)
            {

                _context.Bancos.Remove(bancoToDelete);
               await _context.SaveChangesAsync();
            }

        }


    }
}
