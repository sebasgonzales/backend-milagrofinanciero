using Data.Models;

namespace Services
{
    public interface IPaisService
    {
        Task<Pais> Create(PaisDtoIn newPaisDtoIn);
        Task Delete(int id);
        Task<IEnumerable<PaisDtoOut>> GetAll();
        Task<Pais?> GetById(int id);
        Task<PaisDtoOut?> GetDtoById(int id);
        Task Update(int id, PaisDtoIn pais);
    }
}