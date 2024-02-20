using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Core.DTO.request;
using Core.DTO.response;
using Microsoft.EntityFrameworkCore;
using GeneradorNumeros;
using System.Diagnostics;

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
                //Include(c =>
                //Banco)
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
            int numFijo = 111;
            long numAleatorio= Array.ConvertAll(AlgoritmoGenerador.GenerarNumerosAleatorios(), x => (int)x)[0];
            long numCuenta= long.Parse(numFijo.ToString() + numAleatorio.ToString());

            // Obtengo el código del banco
            var banco = await _context.Banco
                .Where(b => b.Id == newCuentaDto.IdBanco)
                .Select(b => b.Codigo)
                .FirstOrDefaultAsync();
            if (banco == null)
            {
                throw new Exception("El banco especificado no existe.");
            }
            string cbu = banco.ToString() + numCuenta.ToString();

            newCuenta.Numero = numCuenta;
            newCuenta.Cbu = cbu;
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

        /*
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
        
         */

        public async Task<CuentaIdDtoOut> GetIdByCbu(string cbu)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Cbu == cbu)
                .Select(c => new CuentaIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return cuenta;
        }

        public async Task<CuentaIdDtoOut?> GetIdByNumeroCuenta(long numeroCuenta)
        {
            var cuenta = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => new CuentaIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return cuenta;
        }

        public async Task<List<ContactoDtoOut>> GetContactos(long numeroCuenta)
        {
            //obtengo el id de la cuenta
            var cuentaId = await _context.Cuenta
                .Where(c => c.Numero == numeroCuenta)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); //devuelve si encontro o sino default
            if (cuentaId == default)
            {
                return new List<ContactoDtoOut>(); // No esta el cliente, devuelve lista vacia
            }
            //si se encontró los contactos
            var contactos = await _context.Contacto
                .Where(cc => cc.IdCuenta == cuentaId) // cruzo las tablas
                .Select(cc => new ContactoDtoOut
                {

                    Nombre = cc.Nombre,
                    Cbu = cc.Cbu,
                    Banco = cc.Banco.Nombre
                }).ToListAsync();

            return contactos;

        }

    }
}
