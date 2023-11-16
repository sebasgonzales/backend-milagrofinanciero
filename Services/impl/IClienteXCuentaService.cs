using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.impl
{
    public interface IClienteXCuentaService
    {
        Task<ClienteXCuenta> Create(ClienteXCuentaDtoIn newClienteXCuentaDTO);
        Task Delete(int id);
        Task<IEnumerable<ClienteXCuentaDtoOut>> GetAll();
        Task<ClienteXCuenta?> GetById(int id);
        Task<ClienteXCuentaDtoOut?> GetDtoById(int id);
        Task Update(int id, ClienteXCuentaDtoIn clienteXCuenta);
    }
}