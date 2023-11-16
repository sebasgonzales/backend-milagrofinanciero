using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class TipoTransaccionService
    {

        private readonly MilagrofinancieroG1Context _context;
        public TipoTransaccionService(MilagrofinancieroG1Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TipoTransaccionDtoOut>> GetAll()
        {
            return await _context.TipoTransaccions.Select(tt => new TipoTransaccionDtoOut
            {
                Nombre = tt.Nombre
            }).ToListAsync();

        }

        public async Task<TipoTransaccionDtoOut?> GetDtoById(int id)
        {
            return await _context.TipoTransaccions
                .Where(tt => tt.Id == id)
                .Select(tt => new TipoTransaccionDtoOut
                {
                    Nombre = tt.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<TipoTransaccion?> GetById(int id)
        {
            return await _context.TipoTransaccions.FindAsync(id);
        }

        public async Task<TipoTransaccion> Create(TipoTransaccionDtoIn newtipoTransaccionDTO)
        {
            var newtipoTransaccion = new TipoTransaccion();
            newtipoTransaccion.Nombre = newtipoTransaccionDTO.Nombre;

            _context.TipoTransaccions.Add(newtipoTransaccion);
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

                _context.TipoTransaccions.Remove(tipoTransaccionToDelete);
                await _context.SaveChangesAsync();
            }

        }


    }
}