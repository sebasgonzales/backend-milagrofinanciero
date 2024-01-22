﻿using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
namespace Services
{
    public interface ICuentaService
    {
        Task<Cuenta> Create(CuentaDtoIn newCuentaDto);
        Task Delete(int id);
        Task<IEnumerable<CuentaDtoOut>> GetAll();
        Task<Cuenta?> GetById(int id);
        Task<CuentaDtoOut?> GetByIdDto(int id);
        Task Update(int id, CuentaDtoIn cuenta);

        Task<Cuenta> GetCuentaByCbu(long cbu);
        Task<Cuenta> GetCuentaByNumero(long numeroCuenta);

        Task<int?> GetCuentaIdByCbu(long cbu);

        Task<CuentaIdDtoOut?> GetByNumeroCuenta(long numeroCuenta);

    }

}