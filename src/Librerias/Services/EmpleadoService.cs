using System;
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
    public class EmpleadoService : IEmpleadoService
    {
        private readonly milagrofinancierog1Context _context;


        public EmpleadoService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmpleadoDtoOut>> GetAll()
        {
            return await _context.Empleado
            .Select(e => new EmpleadoDtoOut
            {
                Nombre = e.Nombre,
                CuitCuil = e.CuitCuil,
                Legajo = e.Legajo,
                SucursalNombre = e.Sucursal.Nombre
            }).ToListAsync();
        }

        public async Task<Empleado?> GetById(int id)
        {
            var empleado = await _context.Empleado
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();
            return empleado;
        }

        //como el getById lo uso mucho, defino otro endpoint para el dtoOut
        public async Task<EmpleadoDtoOut?> GetDtoById(int id)
        {
            return await _context.Empleado.
            Where(e => e.Id == id).
            Select(e => new EmpleadoDtoOut
            {
                Nombre = e.Nombre,
                CuitCuil = e.CuitCuil,
                Legajo = e.Legajo,
                SucursalNombre = e.Sucursal.Nombre
            }).SingleOrDefaultAsync();
        }
        public async Task<Empleado> Create(EmpleadoDtoIn empleadoDTO)
        {
            var nuevoEmpleado = new Empleado();

            nuevoEmpleado.Legajo = empleadoDTO.Legajo;
            nuevoEmpleado.Nombre = empleadoDTO.Nombre;
            nuevoEmpleado.CuitCuil = empleadoDTO.CuitCuil;
            nuevoEmpleado.IdSucursal = empleadoDTO.IdSucursal;

            _context.Empleado.Add(nuevoEmpleado);
            await _context.SaveChangesAsync();

            return nuevoEmpleado;
        }

        //metodo para actualizar 
        public async Task Update(int id, EmpleadoDtoIn empleado)
        {
            var existingEmpleado = await GetById(id);

            if (existingEmpleado is not null)
            {
                existingEmpleado.Nombre = empleado.Nombre;
                existingEmpleado.CuitCuil = empleado.CuitCuil;
                existingEmpleado.Legajo = empleado.Legajo;
                existingEmpleado.IdSucursal = empleado.IdSucursal;

                await _context.SaveChangesAsync();

            }
        }

        public async Task Delete(int id)
        {
            var empleadoToDelete = await GetById(id);

            if (empleadoToDelete is not null)
            {
                _context.Empleado.Remove(empleadoToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}