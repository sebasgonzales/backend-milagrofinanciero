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
    public class TipoMotivoService : ITipoMotivoService
    {
        private readonly milagrofinancierog1Context _context;
        public TipoMotivoService(milagrofinancierog1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoMotivoDtoOut>> GetAll()
        {
            return await _context.TipoMotivo.Select(tm => new TipoMotivoDtoOut
            {
                Nombre = tm.Nombre
            }).ToListAsync();

        }

        public async Task<TipoMotivoDtoOut?> GetDtoById(int id)
        {
            return await _context.TipoMotivo
                .Where(tm => tm.Id == id)
                .Select(tm => new TipoMotivoDtoOut
                {
                    Nombre = tm.Nombre

                }).SingleOrDefaultAsync();

        }

        public async Task<TipoMotivo?> GetById(int id)
        {
            return await _context.TipoMotivo.FindAsync(id);
        }

        public async Task<TipoMotivo> Create(TipoMotivoDtoIn newTipoMotivoDtoIn)
        {
            var newTipoMotivo = new TipoMotivo();
            newTipoMotivo.Nombre = newTipoMotivoDtoIn.Nombre;

            _context.TipoMotivo.Add(newTipoMotivo);
            await _context.SaveChangesAsync();

            return newTipoMotivo;

        }

        public async Task Update(int id, TipoMotivoDtoIn tipoMotivo)
        {
            var existingTipoMotivo = await GetById(id);

            if (existingTipoMotivo is not null)
            {

                existingTipoMotivo.Nombre = tipoMotivo.Nombre;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var tipoMotivoToDelete = await GetById(id);

            if (tipoMotivoToDelete is not null)
            {

                _context.TipoMotivo.Remove(tipoMotivoToDelete);
                await _context.SaveChangesAsync();
            }

        }
    }
}
