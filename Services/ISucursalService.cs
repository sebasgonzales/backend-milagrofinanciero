using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services
{
    public interface ISucursalService
    {
        Task<Sucursal> Create(SucursalDtoIn newSucursalDTO);
        Task Delete(int id);
        Task<IEnumerable<SucursalDtoOut>> GetAll();
        Task<Sucursal?> GetById(int id);
        Task<SucursalDtoOut?> GetByIdDto(int id);
        Task Update(int id, SucursalDtoIn sucursal);
    }
}