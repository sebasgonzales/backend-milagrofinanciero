using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.impl
{
    public interface ITransaccionService
    {
        Task<Transaccion> Create(TransaccionDtoIn newTransaccionDTO);
        Task Delete(int id);
        Task<IEnumerable<TransaccionDtoOut>> GetAll();
        Task<Transaccion?> GetById(int id);
        Task<TransaccionDtoOut?> GetDtoById(int id);
        Task Update(int id, TransaccionDtoIn transaccion);
    }
}