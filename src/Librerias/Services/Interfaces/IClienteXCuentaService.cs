
namespace Services
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