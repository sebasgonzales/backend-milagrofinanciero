﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class TipoTransaccionService : ITipoTransaccionService
    {

        private readonly milagrofinancierog1Context _context;
        public TipoTransaccionService(milagrofinancierog1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoTransaccionDtoOut>> GetAll()
        {
            return await _context.TipoTransaccion.Select(tt => new TipoTransaccionDtoOut
            {
                Nombre = tt.Nombre
            }).ToListAsync();

        }

        public async Task<TipoTransaccionDtoOut?> GetDtoById(int id)
        {
            return await _context.TipoTransaccion
                .Where(tt => tt.Id == id)
                .Select(tt => new TipoTransaccionDtoOut
                {
                    Nombre = tt.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<TipoTransaccion?> GetById(int id)
        {
            return await _context.TipoTransaccion.FindAsync(id);
        }

        public async Task<TipoTransaccion> Create(TipoTransaccionDtoIn newtipoTransaccionDTO)
        {
            var newtipoTransaccion = new TipoTransaccion();
            newtipoTransaccion.Nombre = newtipoTransaccionDTO.Nombre;

            _context.TipoTransaccion.Add(newtipoTransaccion);
            await _context.SaveChangesAsync();

            return newtipoTransaccion;

        }

        public async Task Update(int id, TipoTransaccionDtoIn tipoTransaccion)
        {
            var existingtipoTransaccion = await GetById(id);

            if (existingtipoTransaccion is not null)
            {

                existingtipoTransaccion.Nombre = tipoTransaccion.Nombre;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var tipoTransaccionToDelete = await GetById(id);

            if (tipoTransaccionToDelete is not null)
            {

                _context.TipoTransaccion.Remove(tipoTransaccionToDelete);
                await _context.SaveChangesAsync();
            }

        }


    }
}
