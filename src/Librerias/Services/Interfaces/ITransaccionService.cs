using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using System.Numerics;
namespace Services
{
    public interface ITransaccionService
    {
        Task<Transaccion> Create(TransaccionDtoIn newTransaccionDTO);
        //Task Delete(int id);
        Task<IEnumerable<TransaccionDtoOut>> GetAll();
        Task<Transaccion?> GetById(int id);
        Task<TransaccionDtoOut?> GetDtoById(int id);
        Task<int> GetSaldo(int numeroCuenta, float monto);
        Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta);
        //Task Update(TransaccionDtoIn transaccion);
        Task<float> ObtenerSaldo(long numeroCuenta);
        
    }
}