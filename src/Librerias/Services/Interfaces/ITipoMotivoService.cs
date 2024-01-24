using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface ITipoMotivoService
    {
        Task<TipoMotivo> Create(TipoMotivoDtoIn newTipoMotivoDtoIn);
        Task Delete(int id);
        Task<IEnumerable<TipoMotivoDtoOut>> GetAll();
        Task<TipoMotivo?> GetById(int id);
        Task<TipoMotivoDtoOut?> GetDtoById(int id);
        Task Update(int id, TipoMotivoDtoIn tipoMotivo);
    }
}