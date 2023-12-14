using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Services
{
    public class CuentaService : ICuentaService
    {
        private readonly milagrofinancierog1Context _context;

        public CuentaService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaDtoOut>> GetAll()
        {
            return await _context.Cuenta
                //Include(c => c.Banco)
                .Select(c => new CuentaDtoOut
                {
                    NumeroCuenta = c.NumeroCuenta,
                    Cbu = c.Cbu,
                    TipoCuenta = c.TipoCuenta.Nombre,
                    Banco = c.Banco.Nombre,
                    Sucursal = c.Sucursal.Nombre
                }).ToListAsync();


        }

        public async Task<CuentaDtoOut?> GetByIdDto(int id)
        {
            return await _context.Cuenta
                .Where(c => c.Id == id)
                .Select(c => new CuentaDtoOut
                {
                    NumeroCuenta = c.NumeroCuenta,
                    Cbu = c.Cbu,
                    TipoCuenta = c.TipoCuenta.Nombre,
                    Banco = c.Banco.Nombre,
                    Sucursal = c.Sucursal.Nombre
                }).SingleOrDefaultAsync();
        }

        public async Task<Cuenta?> GetById(int id)
        {
            return await _context.Cuenta
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Cuenta> Create(CuentaDtoIn newCuentaDto)
        {
            var newCuenta = new Cuenta();

            newCuenta.NumeroCuenta = newCuentaDto.NumeroCuenta;
            newCuenta.Cbu = newCuentaDto.Cbu;
            newCuenta.TipoCuentaId = newCuentaDto.TipoCuentaId;
            newCuenta.BancoId = newCuentaDto.BancoId;
            newCuenta.SucursalId = newCuentaDto.SucursalId;

            _context.Cuenta.Add(newCuenta);
            await _context.SaveChangesAsync();

            return newCuenta;
        }

        public async Task Update(int id, CuentaDtoIn cuenta)
        {
            var cuentaExistente = await GetById(id);
            if (cuentaExistente is null)
            {
                cuentaExistente.NumeroCuenta = cuenta.NumeroCuenta;
                cuentaExistente.Cbu = cuenta.Cbu;
                cuentaExistente.TipoCuentaId = cuenta.TipoCuentaId;
                cuentaExistente.BancoId = cuenta.BancoId;
                cuentaExistente.SucursalId = cuenta.SucursalId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var cuentaParaEliminar = await GetById(id);
            if (cuentaParaEliminar is not null)
            {
                _context.Cuenta.Remove(cuentaParaEliminar);
                await _context.SaveChangesAsync();
            }
        }

    }
}
