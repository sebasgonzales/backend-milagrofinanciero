using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.Impl
{
    public interface ILocalidadService
    {
        Task<Localidad> Create(LocalidadDtoIn newLocalidadDto);
        Task Delete(int id);
        Task<IEnumerable<LocalidadDtoOut>> GetAll();
        Task<Localidad?> GetById(int id);
        Task<LocalidadDtoOut?> GetByIdDto(int id);
        Task Update(int id, LocalidadDtoIn updateLocalidad);
    }
}