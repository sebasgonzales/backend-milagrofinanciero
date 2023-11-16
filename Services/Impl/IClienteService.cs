using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.Impl
{
    public interface IClienteService
    {
        Task<Cliente> Create(ClienteDtoIn newClienteDTO);
        Task Delete(int id);
        Task<IEnumerable<ClienteDtoOut>> GetAll();
        Task<Cliente?> GetById(int id);
        Task<ClienteDtoOut?> GetByIdDto(int id);
        Task Update(int id, ClienteDtoIn cliente);
    }
}