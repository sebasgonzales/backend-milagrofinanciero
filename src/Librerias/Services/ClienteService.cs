using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Services
{
    public class ClienteService : IClienteService
    {
        private readonly milagrofinancierog1Context _context;
        public ClienteService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDtoOut>> GetAll()
        {
            return await _context.Cliente
                .Select(c => new ClienteDtoOut
                {
                    RazonSocial = c.RazonSocial,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    Numero = c.Numero,
                }).ToListAsync();
        }

        // GetById con Dto
        public async Task<ClienteDtoOut?> GetByIdDto(int id)
        {
            return await _context.Cliente
                .Where(c => c.Id == id)
                .Select(c => new ClienteDtoOut
                {
                    RazonSocial = c.RazonSocial,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    Numero = c.Numero,

                }).SingleOrDefaultAsync();
        }

        // GetById sin Dto
        public async Task<Cliente?> GetById(int id)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();
            return cliente;
        }


        public async Task<Cliente> Create(ClienteDtoIn newClienteDTO)
        {
            var newCliente = new Cliente();

            newCliente.RazonSocial = newClienteDTO.RazonSocial;
            newCliente.Alta = newClienteDTO.Alta;
            newCliente.CuitCuil = newClienteDTO.CuitCuil;
            newCliente.Calle = newClienteDTO.Calle;
            newCliente.Departamento = newClienteDTO.Departamento;
            newCliente.Numero = newClienteDTO.Numero;
            newCliente.LocalidadId = newClienteDTO.LocalidadId;

            _context.Cliente.Add(newCliente);
            await _context.SaveChangesAsync();

            return newCliente;
        }
        public async Task Update(int id, ClienteDtoIn cliente)
        {

            var existingClient = await GetById(id);

            if (existingClient is not null)
            {
                existingClient.RazonSocial = cliente.RazonSocial;
                existingClient.Alta = cliente.Alta;
                existingClient.CuitCuil = cliente.CuitCuil;
                existingClient.Calle = cliente.Calle;
                existingClient.Departamento = cliente.Departamento;
                existingClient.Numero = cliente.Numero;
                existingClient.LocalidadId = existingClient.LocalidadId;

                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {

            var clientToDelete = await GetById(id);

            if (clientToDelete is not null)
            {
                _context.Cliente.Remove(clientToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
