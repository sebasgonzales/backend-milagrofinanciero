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
    public class ClienteCuentaService : IClienteCuentaService
    {
        private readonly milagrofinancierog1Context _context;

        public ClienteCuentaService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteCuentaDtoOut>> GetAll()
        {
            return await _context.ClienteCuenta

                .Select(c => new ClienteCuentaDtoOut
                {
                    Titular = c.Titular,
                    Alta = c.Alta,
                    Cliente = c.Cliente.Nombre,
                    Cuenta = c.Cuenta.Numero

                }).ToListAsync();
        }

        public async Task<ClienteCuentaDtoOut?> GetDtoById(int id)
        {
            return await _context.ClienteCuenta
                .Where(c => c.Id == id)
                .Select(c => new ClienteCuentaDtoOut
                {
                    Titular = c.Titular,
                    Alta = c.Alta,
                    Cliente = c.Cliente.Nombre+" "+c.Cliente.Apellido,
                    Cuenta = c.Cuenta.Numero
                }).SingleOrDefaultAsync();
        }

        public async Task<ClienteCuenta?> GetById(int id)
        {
            return await _context.ClienteCuenta.FindAsync(id);
        }

        public async Task<ClienteCuenta> Create(ClienteCuentaDtoIn newClienteCuentaDTO)
        {
            var newClienteCuenta = new ClienteCuenta();

            newClienteCuenta.Titular = newClienteCuentaDTO.Titular;
            newClienteCuenta.Alta = newClienteCuentaDTO.Alta;
            newClienteCuenta.IdCliente = newClienteCuentaDTO.IdCliente;
            newClienteCuenta.IdCuenta = newClienteCuentaDTO.IdCuenta;



            _context.ClienteCuenta.Add(newClienteCuenta);
            await _context.SaveChangesAsync();

            return newClienteCuenta;
        }

        public async Task Update(int id, ClienteCuentaDtoIn clienteCuenta)
        {
            var existingClienteCuenta = await GetById(clienteCuenta.Id);

            if (existingClienteCuenta is not null)
            {
                existingClienteCuenta.Titular = clienteCuenta.Titular;
                existingClienteCuenta.Alta = clienteCuenta.Alta;
                existingClienteCuenta.IdCuenta = clienteCuenta.IdCuenta;
                existingClienteCuenta.IdCuenta = clienteCuenta.IdCuenta;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var clienteCuentaToDelete = await GetById(id);
            if (clienteCuentaToDelete is not null)
            {
                _context.ClienteCuenta.Remove(clienteCuentaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
