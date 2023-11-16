using Microsoft.EntityFrameworkCore;
using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Services;

public class SucursalService : ISucursalService
{
    private readonly MilagrofinancieroG1Context _context;
    public SucursalService(MilagrofinancieroG1Context context)
    { _context = context; }

    public async Task<IEnumerable<SucursalDtoOut>> GetAll()
    {
        return await _context.Sucursal
            .Select(s => new SucursalDtoOut
            {
                Nombre = s.Nombre,
                Cp = s.Cp,
                Calle = s.Calle,
                Departamento = s.Departamento,
                Numero = s.Numero,
                Localidad = s.Localidad.Cp,

            }).ToListAsync();
    }

    // GetById con Dto
    public async Task<SucursalDtoOut?> GetByIdDto(int id)
    {
        return await _context.Sucursal
            .Where(s => s.Id == id)
            .Select(s => new SucursalDtoOut
            {
                Nombre = s.Nombre,
                Cp = s.Cp,
                Calle = s.Calle,
                Departamento = s.Departamento,
                Numero = s.Numero,
                Localidad = s.Localidad.Cp,

            }).SingleOrDefaultAsync();
    }

    // GetById sin Dto
    public async Task<Sucursal?> GetById(int id)
    {
        var sucursal = await _context.Sucursal
            .Where(s => s.Id == id)
            .SingleOrDefaultAsync();
        return sucursal;
    }


    public async Task<Sucursal> Create(SucursalDtoIn newSucursalDTO)
    {
        var newSucursal = new Sucursal();

        newSucursal.Nombre = newSucursalDTO.Nombre;
        newSucursal.Cp = newSucursalDTO.Cp;
        newSucursal.Calle = newSucursalDTO.Calle;
        newSucursal.Departamento = newSucursalDTO.Departamento;
        newSucursal.Numero = newSucursalDTO.Numero;
        newSucursal.CuentaId = newSucursalDTO.CuentaId;
        newSucursal.LocalidadId = newSucursalDTO.LocalidadId;

        _context.Sucursal.Add(newSucursal);
        await _context.SaveChangesAsync();

        return newSucursal;
    }
    public async Task Update(int id, SucursalDtoIn sucursal)
    {

        var existingSucursal = await GetById(id);

        if (existingSucursal is not null)
        {
            existingSucursal.Nombre = sucursal.Nombre;
            existingSucursal.Cp = sucursal.Cp;
            existingSucursal.Calle = sucursal.Calle;
            existingSucursal.Departamento = sucursal.Departamento;
            existingSucursal.Numero = sucursal.Numero;
            existingSucursal.CuentaId = sucursal.CuentaId;
            existingSucursal.LocalidadId = existingSucursal.LocalidadId;

            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {

        var sucursalToDelete = await GetById(id);

        if (sucursalToDelete is not null)
        {
            _context.Sucursal.Remove(sucursalToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
