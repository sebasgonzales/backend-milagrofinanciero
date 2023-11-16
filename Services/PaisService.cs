
using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class PaisService
    {

        private readonly MilagrofinancieroG1Context _context;
        public PaisService(MilagrofinancieroG1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<PaisDtoOut>> GetAll()
        {
            return await _context.Pais.Select(p => new PaisDtoOut
            {
                Nombre = p.Nombre
            }).ToListAsync();

        }

        public async Task<PaisDtoOut?> GetDtoById(int id)
        {
            return await _context.Pais
                .Where(b => b.Id == id)
                .Select(b => new PaisDtoOut
                {
                    Nombre = b.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<Pais?> GetById(int id)
        {
            return await _context.Pais.FindAsync(id);
        }

        public async Task<Pais> Create(PaisDtoIn newPaisDtoIn)
        {
            var newPais = new Pais();
            newPais.Nombre = newPaisDtoIn.Nombre;

            _context.Pais.Add(newPais);
            await _context.SaveChangesAsync();

            return newPais;

        }

        public async Task Update(int id, PaisDtoIn pais)
        {
            var existingPais = await GetById(id);

            if (existingPais is not null)
            {

                existingPais.Nombre = pais.Nombre;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var paisToDelete = await GetById(id);

            if (paisToDelete is not null)
            {

                _context.Pais.Remove(paisToDelete);
                await _context.SaveChangesAsync();
            }

        }


    }
}
