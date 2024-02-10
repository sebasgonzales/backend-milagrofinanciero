using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IClienteService
    {
        Task<Cliente> Create(ClienteDtoIn newClienteDTO);
        Task Delete(int id);
        Task<IEnumerable<ClienteDtoOut>> GetAll();
        Task<string> GetNombre(string cuitCuil);
        Task<Cliente?> GetById(int id);
        Task<ClienteDtoOut?> GetByIdDto(int id);
        Task Update(int id, ClienteDtoIn cliente);

        Task<List<CuentaDtoOut>> GetCuentasByCuitCuil(string cuitCuil);
    }
}