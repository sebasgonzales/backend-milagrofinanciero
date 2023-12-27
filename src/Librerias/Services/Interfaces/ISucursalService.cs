using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
namespace Services
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