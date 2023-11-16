using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services;

public class EmpleadoService
{

    private readonly MilagrofinancieroG1Context _context;
 

    public EmpleadoService(MilagrofinancieroG1Context context)
    {
    _context = context;
    }

    public async Task<IEnumerable<EmpleadoDtoOut>> GetAll()
    {
        return await _context.Empleados.Select(e => new EmpleadoDtoOut
        {
            CuitCuil = e.CuitCuil,
            Legajo = e.Legajo,
            SucursalNombre = e.Sucursal != null ? e.Sucursal.Nombre : "" // si la sucursal es distin ta de null, asigno el nombre, sino le asigno "", el : es el sino
        }).ToListAsync();
    }

    public async Task<Empleado?> GetById(int id)
    {
        return await _context.Empleados.FirstAsync();
    }
    
    //como el getById lo uso mucho, defino otro endpoint para el dtoOut
    public async Task<EmpleadoDtoOut?> GetDtoById(int id)
    {
        return await _context.Empleados.
        Where(e => e.Id == id).
        Select(e => new EmpleadoDtoOut
        {
            CuitCuil = e.CuitCuil,
            Legajo = e.Legajo,
            SucursalNombre = e.Nombre
        }).SingleOrDefaultAsync();
    }
    public async Task<Empleado> Create(EmpleadoDtoIn empleadoDTO)
    {
        var nuevoempleado = new Empleado();

        nuevoempleado.Id = empleadoDTO.Id;
        nuevoempleado.Legajo = empleadoDTO.Legajo;
        nuevoempleado.Nombre = empleadoDTO.Nombre;
        nuevoempleado.CuitCuil = empleadoDTO.CuitCuil;

        await _context.Empleados.AddAsync(nuevoempleado);
        await _context.SaveChangesAsync();

        return nuevoempleado;
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

                await _context.SaveChangesAsync();

            }

      
    }


    public async Task Delete(int id)
    {
        var empleadoToDelete = await GetById(id);

        if (empleadoToDelete is not null)
        {
            _context.Empleados.Remove(empleadoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
