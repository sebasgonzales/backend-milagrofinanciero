using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IBancoService
    {
        Task<Banco> Create(BancoDtoIn newBancoDTO);
        Task Delete(int id);
        Task<IEnumerable<BancoDtoOut>> GetAll();
        Task<Banco?> GetById(int id);
        Task<BancoDtoOut?> GetDtoById(int id);
        Task Update(int id, BancoDtoIn banco);

        Task<BancoIdDtoOut?> GetIdByNombre(string nombre);
    }
}