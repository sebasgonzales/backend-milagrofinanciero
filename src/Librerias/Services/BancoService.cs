using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Services
{
    public class BancoService : IBancoService
    {

        private readonly milagrofinancierog1Context _context;
        public BancoService(milagrofinancierog1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<BancoDtoOut>> GetAll()
        {
            return await _context.Bancos.Select(b => new BancoDtoOut
            {
                Nombre = b.Nombre
            }).ToListAsync();

        }

        public async Task<BancoDtoOut?> GetDtoById(int id)
        {
            return await _context.Bancos
                .Where(b => b.Id == id)
                .Select(b => new BancoDtoOut
                {
                    Nombre = b.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<Banco?> GetById(int id)
        {
            return await _context.Bancos.FindAsync(id);
        }

        public async Task<Banco> Create(BancoDtoIn newBancoDTO)
        {
            var newBanco = new Banco();
            newBanco.Nombre = newBancoDTO.Nombre;

            _context.Bancos.Add(newBanco);
            await _context.SaveChangesAsync();

            return newBanco;

        }

        public async Task Update(int id, BancoDtoIn banco)
        {
            var existingBanco = await GetById(id);

            if (existingBanco is not null)
            {

                existingBanco.Nombre = banco.Nombre;
                await _context.SaveChangesAsync();
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
