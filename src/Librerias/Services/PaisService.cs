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
    public class PaisService : IPaisService
    {

        private readonly milagrofinancierog1Context _context;
        public PaisService(milagrofinancierog1Context context)
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
                .Where(p => p.Id == id)
                .Select(p => new PaisDtoOut
                {
                    Nombre = p.Nombre
                  
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
