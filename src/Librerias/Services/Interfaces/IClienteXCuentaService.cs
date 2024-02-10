using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IClienteCuentaService
    {
        Task<ClienteCuenta> Create(ClienteCuentaDtoIn newClienteCuentaDTO);
        Task Delete(int id);
        Task<IEnumerable<ClienteCuentaDtoOut>> GetAll();
        Task<ClienteCuenta?> GetById(int id);
        Task<ClienteCuentaDtoOut?> GetDtoById(int id);
        Task Update(int id, ClienteCuentaDtoIn clienteCuenta);
    }
}