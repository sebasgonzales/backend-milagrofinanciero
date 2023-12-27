using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
namespace Services
{
    public interface IEmpleadoService
    {
        Task<Empleado> Create(EmpleadoDtoIn empleadoDTO);
        Task Delete(int id);
        Task<IEnumerable<EmpleadoDtoOut>> GetAll();
        Task<Empleado?> GetById(int id);
        Task<EmpleadoDtoOut?> GetDtoById(int id);
        Task Update(int id, EmpleadoDtoIn empleado);
    }
}