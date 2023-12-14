using Data.Models;
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
    public class LocalidadService : ILocalidadService
    {
        private readonly milagrofinancierog1Context _context;
        public LocalidadService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        // GetAll
        public async Task<IEnumerable<LocalidadDtoOut>> GetAll()
        {
            return await _context.Localidad
                .Select(l => new LocalidadDtoOut
                {
                    Nombre = l.Nombre,
                    CodigoPostal = l.CodigoPostal,
                    NombreProvincia = l.Provincia.Nombre
                }).ToListAsync();
        }

        // GetById con Dto
        public async Task<LocalidadDtoOut?> GetByIdDto(int id)
        {
            return await _context.Localidad
                .Where(l => l.Id == id)
                .Select(l => new LocalidadDtoOut
                {
                    Nombre = l.Nombre,
                    CodigoPostal = l.CodigoPostal,
                    NombreProvincia = l.Provincia.Nombre
                }).SingleOrDefaultAsync();
        }

        // GetById sin Dto
        public async Task<Localidad?> GetById(int id)
        {
            var localidad = await _context.Localidad
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync();
            return localidad;
        }

        // Post
        public async Task<Localidad> Create(LocalidadDtoIn newLocalidadDto)
        {
            var newLocalidad = new Localidad();
            newLocalidad.Nombre = newLocalidadDto.Nombre;
            newLocalidad.CodigoPostal = newLocalidadDto.CodigoPostal;
            newLocalidad.IdProvincia = newLocalidadDto.IdProvincia;

            _context.Localidad.Add(newLocalidad);
            await _context.SaveChangesAsync();

            return newLocalidad;
        }

        // Update 
        public async Task Update(int id, LocalidadDtoIn updateLocalidad)
        {
            var existingLocalidad = await GetById(id);

            if (existingLocalidad is not null)
            {
                existingLocalidad.Nombre = updateLocalidad.Nombre;
                existingLocalidad.CodigoPostal = updateLocalidad.CodigoPostal;
                existingLocalidad.IdProvincia = updateLocalidad.IdProvincia;

                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task Delete(int id)
        {
            var localidadToDelete = await GetById(id);
            if (localidadToDelete is not null)
            {
                _context.Localidad.Remove(localidadToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
