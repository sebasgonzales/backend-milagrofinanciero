using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services.impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_milagrofinanciero.Services
{
    public class ClienteXCuentaService : IClienteXCuentaService
    {
        private readonly MilagrofinancieroG1Context _context;

        public ClienteXCuentaService(MilagrofinancieroG1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteXCuentaDtoOut>> GetAll()
        {
            return await _context.ClienteXCuenta

                .Select(c => new ClienteXCuentaDtoOut
                {
                    Rol = c.Rol,
                    Alta = c.Alta,
                    Cliente = c.Cliente.RazonSocial,
                    Cuenta = c.Cuenta.NumeroCuenta
                }).ToListAsync();
        }

        public async Task<ClienteXCuentaDtoOut?> GetDtoById(int id)
        {
            return await _context.ClienteXCuenta
                .Where(c => c.Id == id)
                .Select(c => new ClienteXCuentaDtoOut
                {
                    Rol = c.Rol,
                    Alta = c.Alta,
                    Cliente = c.Cliente.RazonSocial,
                    Cuenta = c.Cuenta.NumeroCuenta
                }).SingleOrDefaultAsync();
        }

        public async Task<ClienteXCuenta?> GetById(int id)
        {
            return await _context.ClienteXCuenta.FindAsync(id);
        }

        public async Task<ClienteXCuenta> Create(ClienteXCuentaDtoIn newClienteXCuentaDTO)
        {
            var newClienteXCuenta = new ClienteXCuenta();

            newClienteXCuenta.Rol = newClienteXCuentaDTO.Rol;
            newClienteXCuenta.Alta = newClienteXCuentaDTO.Alta;
            newClienteXCuenta.ClienteId = newClienteXCuentaDTO.ClienteId;
            newClienteXCuenta.CuentaId = newClienteXCuentaDTO.CuentaId;



            _context.ClienteXCuenta.Add(newClienteXCuenta);
            await _context.SaveChangesAsync();

            return newClienteXCuenta;
        }

        public async Task Update(int id, ClienteXCuentaDtoIn clienteXCuenta)
        {
            var existingClienteXCuenta = await GetById(clienteXCuenta.Id);

            if (existingClienteXCuenta is not null)
            {
                existingClienteXCuenta.Rol = clienteXCuenta.Rol;
                existingClienteXCuenta.Alta = clienteXCuenta.Alta;
                existingClienteXCuenta.ClienteId = clienteXCuenta.ClienteId;
                existingClienteXCuenta.CuentaId = clienteXCuenta.CuentaId;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var clienteXCuentaToDelete = await GetById(id);
            if (clienteXCuentaToDelete is not null)
            {
                _context.ClienteXCuenta.Remove(clienteXCuentaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
