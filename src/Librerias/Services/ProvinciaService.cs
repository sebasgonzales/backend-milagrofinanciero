﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly milagrofinancierog1Context _context;

        public ProvinciaService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProvinciaDtoOut>> GetAll()
        {
            return await _context.Provincia

                .Select(p => new ProvinciaDtoOut
                {
                    Nombre = p.Nombre,
                    Pais = p.Pais.Nombre
                }).ToListAsync();
        }

        public async Task<ProvinciaDtoOut?> GetDtoById(int id)
        {
            return await _context.Provincia
                .Where(p => p.Id == id)
                .Select(p => new ProvinciaDtoOut
                {
                    Nombre = p.Nombre,
                    Pais = p.Pais.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Provincia?> GetById(int id)
        {
            return await _context.Provincia.FindAsync(id);
        }

        public async Task<Provincia> Create(ProvinciaDtoIn newProvinciaDTO)
        {
            var newProvincia = new Provincia();

            newProvincia.Nombre = newProvinciaDTO.Nombre;
            newProvincia.IdPais = newProvinciaDTO.IdPais;


            _context.Provincia.Add(newProvincia);
            await _context.SaveChangesAsync();

            return newProvincia;
        }

        public async Task Update(int id, ProvinciaDtoIn provincia)
        {
            var existingProvincia = await GetById(provincia.Id);

            if (existingProvincia is not null)
            {
                existingProvincia.Nombre = provincia.Nombre;
                existingProvincia.IdPais = provincia.IdPais;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var provinciaToDelete = await GetById(id);
            if (provinciaToDelete is not null)
            {
                _context.Provincia.Remove(provinciaToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetProvinciasNombre()
        {
            try
            {
                var provincias = await _context.Provincia
                    .Select(p => p.Nombre)
                    .Distinct()
                    .ToListAsync();

                return provincias;
            }
            catch (Exception ex)
            {
                // Manejar errores, como loguearlos o lanzar excepciones
                throw new Exception("Error al obtener las provincias", ex);
            }
        }
    }
}
