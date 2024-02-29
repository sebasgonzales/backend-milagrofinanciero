using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using System.Numerics;
namespace Services
{
    public interface ITransaccionService
    {
        Task<Transaccion> CreateTransaccionInterna(TransaccionDtoIn newTransaccionDTO);
        //Task Delete(int id);
        Task<IEnumerable<TransaccionDtoOut>> GetAll();
        Task<Transaccion?> GetById(int id);
        Task<TransaccionDtoOut?> GetDtoById(int id);
        Task<int> VerificadorSaldo(long numeroCuenta, float monto);
        Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta);
        //Task Update(TransaccionDtoIn transaccion);
        Task<float> ObtenerSaldo(long numeroCuenta);

        Task<Transaccion> CreateTransaccionExterna(TransaccionExternaDtoIn newTransaccionExternaDTO);
    }
}