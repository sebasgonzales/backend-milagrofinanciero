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
                    NumeroCuenta = c.Numero,
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
                    NumeroCuenta = c.Numero,
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

            newCuenta.Numero = newCuentaDto.Numero;
            newCuenta.Cbu = newCuentaDto.Cbu;
            newCuenta.IdTipoCuenta = newCuentaDto.IdTipoCuenta;
            newCuenta.IdBanco = newCuentaDto.IdBanco;
            newCuenta.IdSucursal = newCuentaDto.IdSucursal;

            _context.Cuenta.Add(newCuenta);
            await _context.SaveChangesAsync();

            return newCuenta;
        }

        public async Task Update(int id, CuentaDtoIn cuenta)
        {
            var cuentaExistente = await GetById(id);
            if (cuentaExistente is not null)
            {
                cuentaExistente.Numero = cuenta.Numero;
                cuentaExistente.Cbu = cuenta.Cbu;
                cuentaExistente.IdTipoCuenta = cuenta.IdTipoCuenta;
                cuentaExistente.IdBanco = cuenta.IdBanco;
                cuentaExistente.IdSucursal = cuenta.IdSucursal;

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

        public async Task<Cuenta> GetCuentaByCbu(long cbu)
        {
            return await _context.Cuenta
                .FirstOrDefaultAsync(c => c.Cbu == cbu);
        }
        public async Task<Cuenta> GetCuentaByNumero(long numeroCuenta)
        {
            return await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> GetCuentaIdByCbu(long cbu)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Cbu == cbu)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return cuenta;
        }

        public async Task<int?> GetCuentaIdByNumeroCuenta(int numeroCuenta)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return cuenta;
        }
    }
}
