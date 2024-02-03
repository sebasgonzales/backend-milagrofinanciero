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
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
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
                    AlturaCalle = c.AlturaCalle,
                    Username= c.Username,
                    Localidad = c.Localidad.Nombre
                }).SingleOrDefaultAsync();
        }


        // GetNombre nuevo
        public async Task<string> GetNombre(string username)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Username == username)
                .SingleOrDefaultAsync();
            return cliente.RazonSocial;
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
            newCliente.AlturaCalle = newClienteDTO.AlturaCalle;
            newCliente.Username = newClienteDTO.Username;
            newCliente.Password = newClienteDTO.Password;
            newCliente.IdLocalidad = newClienteDTO.IdLocalidad;

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
                existingClient.AlturaCalle = cliente.AlturaCalle;
                existingClient.Username = cliente.Username;
                existingClient.Password = cliente.Password;
                existingClient.IdLocalidad = existingClient.IdLocalidad;

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

        public async Task<List<CuentaDtoOut>> GetCuentasByCuitCuil(string cuitCuil)
        {
            //obtengo el id del cliente
            var clienteId = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); //devuelve si encontro o sino default

            if (clienteId == default)
            {
                return new List<CuentaDtoOut>(); // No esta el cliente, devuelve lista vacia
            }

            //si se encontro el cliente
            var cuentas = await _context.ClienteXcuenta
                .Where(cc => cc.IdCliente == clienteId) // cruzo las tablas
                .Select(cc => new CuentaDtoOut
                {
                    NumeroCuenta = cc.Cuenta.Numero,
                    Cbu = cc.Cuenta.Cbu,
                    TipoCuenta = cc.Cuenta.TipoCuenta.Nombre,
                    Banco = cc.Cuenta.Banco.Nombre,
                    Sucursal = cc.Cuenta.Sucursal.Nombre
                }).ToListAsync();

            return cuentas;
        }




    }
}

