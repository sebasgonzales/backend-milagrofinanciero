using Data.Models;

namespace Services
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