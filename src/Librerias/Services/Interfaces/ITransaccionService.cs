﻿using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using System.Numerics;
namespace Services
{
    public interface ITransaccionService
    {
        Task<Transaccion> Create(TransaccionDtoIn newTransaccionDTO);
        Task Delete(int id);
        Task<IEnumerable<TransaccionDtoOut>> GetAll();
        Task<Transaccion?> GetById(int id);
        Task<TransaccionDtoOut?> GetDtoById(int id);
        Task Update(int id, TransaccionDtoIn transaccion);
        Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta);
    }
}