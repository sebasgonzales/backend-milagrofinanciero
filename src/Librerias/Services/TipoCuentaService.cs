using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Services
{
    public class TipoCuentaService : ITipoCuentaService
    {

        private readonly milagrofinancierog1Context _context;
        public TipoCuentaService(milagrofinancierog1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoCuentaDtoOut>> GetAll()
        {
            return await _context.TipoCuenta.Select(tc => new TipoCuentaDtoOut
            {
                Id = tc.Id,
                Nombre = tc.Nombre,
            }).ToListAsync();


        }

        public async Task<TipoCuentaDtoOut?> GetDtoById(int id)  //no duelve una lista, sino un objeto
        {
            return await _context.TipoCuenta
                .Where(tc => tc.Id == id)
                .Select(tc => new TipoCuentaDtoOut
                {
                    Id = tc.Id,
                    Nombre = tc.Nombre,
                }).SingleOrDefaultAsync();


        }

        public async Task<TipoCuenta?> GetById(int id) //devuelve lista tipo cuenta
        {
            return await _context.TipoCuenta.FindAsync(id);
        }

        public async Task<TipoCuenta> Create(TipoCuentaDtoIn newtipoCuentaDTO)
        {
            var newtipoCuenta = new TipoCuenta();

            //newtipoCuenta.Id = newtipoCuentaDTO.Id; //ID NO PQ ES AUTOINCREMENTAL
            newtipoCuenta.Nombre = newtipoCuentaDTO.Nombre;


            _context.TipoCuenta.Add(newtipoCuenta);
            await _context.SaveChangesAsync();

            return newtipoCuenta;

        }

        public async Task Update(int id, TipoCuentaDtoIn tipoCuenta)
        {
            var existingtipoCuenta = await GetById(id);

            if (existingtipoCuenta is not null)
            {

                existingtipoCuenta.Nombre = tipoCuenta.Nombre;
                // existingtipoCuenta.Alta = tipoCuenta.Alta;
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
    internal class TipoCuentaService
    {
    }
}
