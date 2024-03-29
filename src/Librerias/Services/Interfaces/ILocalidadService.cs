﻿using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface ILocalidadService
    {
        Task<Localidad> Create(LocalidadDtoIn newLocalidadDto);
        Task Delete(int id);
        Task<IEnumerable<LocalidadDtoOut>> GetAll();
        Task<Localidad?> GetById(int id);
        Task<LocalidadDtoOut?> GetByIdDto(int id);
        Task<IEnumerable<LocalidadDtoOut>> GetLocalidades(string nombreProvincia);
        Task Update(int id, LocalidadDtoIn updateLocalidad);
    }
}