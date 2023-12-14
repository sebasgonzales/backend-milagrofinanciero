using Data.Models;

namespace Services
{
    public interface ITipoTransaccionService
    {
        Task<TipoTransaccion> Create(TipoTransaccionDtoIn newtipoTransaccionDTO);
        Task Delete(int id);
        Task<IEnumerable<TipoTransaccionDtoOut>> GetAll();
        Task<TipoTransaccion?> GetById(int id);
        Task<TipoTransaccionDtoOut?> GetDtoById(int id);
        Task Update(int id, TipoTransaccionDtoIn tipoTransaccion);
    }
}