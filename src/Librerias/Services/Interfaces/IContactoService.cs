using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IContactoService
    {
        Task<Contacto> Create(ContactoDtoIn newContactoDto);
        Task Delete(int id);
        Task<IEnumerable<ContactoDtoOut>> GetAll();
        Task<Contacto?> GetById(int id);
        Task<ContactoDtoOut?> GetDtoById(int id);
        Task Update(int id, ContactoDtoIn contacto);

        Task<ContactoIdDtoOut?> GetIdByCbu(long cbu);

    }
}