using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.Impl
{
    public interface IProvinciaService
    {
        Task<Provincia> Create(ProvinciaDtoIn newProvinciaDTO);
        Task Delete(int id);
        Task<IEnumerable<ProvinciaDtoOut>> GetAll();
        Task<Provincia?> GetById(int id);
        Task<ProvinciaDtoOut?> GetDtoById(int id);
        Task Update(int id, ProvinciaDtoIn provincia);
    }
}