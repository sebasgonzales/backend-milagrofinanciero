﻿using Data.Models;

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
    }
}