using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.impl
{
    public interface ICuentaService
    {    
        Task<IEnumerable<CuentaDtoOut>> GetAll();
        Task<CuentaDtoOut> GetById(int id);
        Task<Cuenta> Create(CuentaDtoIn newCuentaDto);

    }
}
