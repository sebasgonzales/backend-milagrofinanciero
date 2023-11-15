using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class LocalidadService : ILocalidadService
    {
        private readonly MilagrofinancieroG1Context _context;
        public LocalidadService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        // GetAll
        public async Task<IEnumerable<LocalidadDtoOut>> GetAll()
        {
            return await _context.Localidad
                .Select(l => new LocalidadDtoOut
                {
                    CodigoPostal = l.Cp,
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
                    CodigoPostal = l.Cp,
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
            newLocalidad.Cp = newLocalidadDto.CodigoPostal;

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
                existingLocalidad.Cp = updateLocalidad.CodigoPostal;

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
