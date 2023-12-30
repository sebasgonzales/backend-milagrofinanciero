using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface ITransaccionService
    {
        Task<Transaccion> Create(TransaccionDtoIn newTransaccionDTO);
        Task Delete(int id);
        Task<IEnumerable<TransaccionDtoOut>> GetAll();
        Task<Transaccion?> GetById(int id);
        Task<TransaccionDtoOut?> GetDtoById(int id);
        int GetSaldo(long cbu, float monto);
        Task Update(TransaccionDtoIn transaccion);
    }
}