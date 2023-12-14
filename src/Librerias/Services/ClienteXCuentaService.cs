using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ClienteXCuentaService : IClienteXCuentaService
    {
        private readonly milagrofinancierog1Context _context;

        public ClienteXCuentaService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteXCuentaDtoOut>> GetAll()
        {
            return await _context.ClienteXcuenta

                .Select(c => new ClienteXCuentaDtoOut
                {
                    //Rol = c.Rol,
                    Alta = c.Alta,
                    Cliente = c.Cliente.RazonSocial,
                    Cuenta = c.Cuenta.Numero

                }).ToListAsync();
        }

        public async Task<ClienteXCuentaDtoOut?> GetDtoById(int id)
        {
            return await _context.ClienteXcuenta
                .Where(c => c.Id == id)
                .Select(c => new ClienteXCuentaDtoOut
                {
                    //Rol = c.Rol,
                    Alta = c.Alta,
                    Cliente = c.Cliente.RazonSocial,
                    Cuenta = c.Cuenta.Numero
                }).SingleOrDefaultAsync();
        }

        public async Task<ClienteXcuenta?> GetById(int id)
        {
            return await _context.ClienteXcuenta.FindAsync(id);
        }

        public async Task<ClienteXcuenta> Create(ClienteXCuentaDtoIn newClienteXCuentaDTO)
        {
            var newClienteXCuenta = new ClienteXcuenta();

            //newClienteXCuenta.Rol = newClienteXCuentaDTO.Rol;
            newClienteXCuenta.Alta = newClienteXCuentaDTO.Alta;
            newClienteXCuenta.IdCliente = newClienteXCuentaDTO.IdCliente;
            newClienteXCuenta.IdCuenta = newClienteXCuentaDTO.IdCuenta;



            _context.ClienteXcuenta.Add(newClienteXCuenta);
            await _context.SaveChangesAsync();

            return newClienteXCuenta;
        }

        public async Task Update(int id, ClienteXCuentaDtoIn clienteXCuenta)
        {
            var existingClienteXCuenta = await GetById(clienteXCuenta.Id);

            if (existingClienteXCuenta is not null)
            {
                //existingClienteXCuenta.Rol = clienteXCuenta.Rol;
                existingClienteXCuenta.Alta = clienteXCuenta.Alta;
                existingClienteXCuenta.IdCuenta = clienteXCuenta.IdCuenta;
                existingClienteXCuenta.IdCuenta = clienteXCuenta.IdCuenta;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var clienteXCuentaToDelete = await GetById(id);
            if (clienteXCuentaToDelete is not null)
            {
                _context.ClienteXcuenta.Remove(clienteXCuentaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
