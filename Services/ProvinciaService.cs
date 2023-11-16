using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class ProvinciaService
    {
        private readonly MilagrofinancieroG1Context _context;

        public ProvinciaService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProvinciaDtoOut>> GetAll()
        {
            return await _context.Provincia

                .Select(p => new ProvinciaDtoOut
                {
                    Nombre = p.Nombre,
                    Pais = p.Pais.Nombre
                }).ToListAsync();
        }

        public async Task<ProvinciaDtoOut?> GetDtoById(int id)
        {
            return await _context.Provincia
                .Where(p => p.Id == id)
                .Select(p => new ProvinciaDtoOut
                {
                    Nombre = p.Nombre,
                    Pais = p.Pais.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Provincia?> GetById(int id)
        {
            return await _context.Provincia.FindAsync(id);
        }

        public async Task<Provincia> Create(ProvinciaDtoIn newProvinciaDTO)
        {
            var newProvincia = new Provincia();

            newProvincia.Nombre = newProvinciaDTO.Nombre;
            newProvincia.PaisId = newProvinciaDTO.PaisId;
            

            _context.Provincia.Add(newProvincia);
            await _context.SaveChangesAsync();

            return newProvincia;
        }

        public async Task Update(int id, ProvinciaDtoIn provincia)
        {
            var existingProvincia = await GetById(provincia.Id);

            if (existingProvincia is not null)
            {
                existingProvincia.Nombre = provincia.Nombre;
                existingProvincia.PaisId = provincia.PaisId;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var provinciaToDelete = await GetById(id);
            if (provinciaToDelete is not null)
            {
                _context.Provincia.Remove(provinciaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
