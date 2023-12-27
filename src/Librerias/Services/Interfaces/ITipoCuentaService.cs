using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
namespace Services
{
    public interface ITipoCuentaService
    {
        Task<TipoCuenta> Create(TipoCuentaDtoIn newtipoCuentaDTO);
        Task Delete(int id);
        Task<IEnumerable<TipoCuentaDtoOut>> GetAll();
        Task<TipoCuenta?> GetById(int id);
        Task<TipoCuentaDtoOut?> GetDtoById(int id);
        Task Update(int id, TipoCuentaDtoIn tipoCuenta);
    }
}