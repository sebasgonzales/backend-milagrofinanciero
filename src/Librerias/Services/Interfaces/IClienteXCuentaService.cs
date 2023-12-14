using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IClienteXCuentaService
    {
        Task<ClienteXcuenta> Create(ClienteXCuentaDtoIn newClienteXCuentaDTO);
        Task Delete(int id);
        Task<IEnumerable<ClienteXCuentaDtoOut>> GetAll();
        Task<ClienteXcuenta?> GetById(int id);
        Task<ClienteXCuentaDtoOut?> GetDtoById(int id);
        Task Update(int id, ClienteXCuentaDtoIn clienteXCuenta);
    }
}