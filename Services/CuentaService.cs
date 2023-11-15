using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services.impl;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly MilagrofinancieroG1Context _context;

        public CuentaService(MilagrofinancieroG1Context context)
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
                    Banco = c.Banco.Nombre
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
                    Banco = c.Banco.Nombre
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
            newCuenta.TipoCuenta = newCuentaDto.TipoCuenta;
            newCuenta.Banco = newCuentaDto.Banco;

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
                cuentaExistente.TipoCuenta = cuenta.TipoCuenta;
                cuentaExistente.Banco = cuenta.Banco;

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