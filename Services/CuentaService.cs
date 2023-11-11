﻿using backend_milagrofinanciero.Data;
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

        public async Task<CuentaDtoOut> GetById(int id)
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

        public async Task<Cuenta> Create(CuentaDtoIn newCuentaDto)
        {
            var newCuenta = new Cuenta();

            newCuenta.NumeroCuenta = newCuentaDto.numeroCuenta;
            newCuenta.Cbu = newCuentaDto.cbu;
            newCuenta.TipoCuenta = newCuentaDto.TipoCuenta;
            newCuenta.Banco = newCuentaDto.Banco;

            _context.Cuenta.Add(newCuenta);
            await _context.SaveChangesAsync();

            return newCuenta;
        }
    }
}