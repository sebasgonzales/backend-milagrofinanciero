using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services.Impl
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