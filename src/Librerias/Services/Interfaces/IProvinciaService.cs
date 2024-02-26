using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using System.Threading.Tasks;
namespace Services
{
    public interface IProvinciaService
    {
        Task<Provincia> Create(ProvinciaDtoIn newProvinciaDTO);
        Task Delete(int id);
        Task<IEnumerable<ProvinciaDtoOut>> GetAll();
        Task<Provincia?> GetById(int id);
        Task<ProvinciaDtoOut?> GetDtoById(int id);
        Task Update(int id, ProvinciaDtoIn provincia);
        Task<IEnumerable<string>> GetProvinciasNombre();
    }
}