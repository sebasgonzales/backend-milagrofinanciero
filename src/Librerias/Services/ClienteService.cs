using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Hashing;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ClienteService : IClienteService
    {
        private readonly milagrofinancierog1Context _context;
        private readonly Hashear _hashing;
        public ClienteService(milagrofinancierog1Context context, Hashear hashing)
        {
            _context = context;
            _hashing = hashing;
        }

        public async Task<IEnumerable<ClienteDtoOut>> GetAll()
        {
            return await _context.Cliente
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
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
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).SingleOrDefaultAsync();
        }


        // GetNombre nuevo
        public async Task<string> GetNombre(string cuitCuil)
        {
            var cliente = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).SingleOrDefaultAsync();
            return cliente.Nombre;
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

            newCliente.Nombre = newClienteDTO.Nombre;
            newCliente.Apellido = newClienteDTO.Apellido;
            newCliente.Alta = newClienteDTO.Alta;
            newCliente.CuitCuil = newClienteDTO.CuitCuil;
            newCliente.Calle = newClienteDTO.Calle;
            newCliente.Departamento = newClienteDTO.Departamento;
            newCliente.AlturaCalle = newClienteDTO.AlturaCalle;
            newCliente.Username = newClienteDTO.Username;
            newCliente.Password = _hashing.HashearConSHA256(newClienteDTO.Password);
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
                existingClient.Nombre = cliente.Nombre;
                existingClient.Apellido = cliente.Apellido;
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
            var cuentas = await _context.ClienteCuenta
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

        public async Task<List<ClienteDtoOut>> GetClienteByCuitCuil(string cuitCuil)
        {
            // Obtengo el id del cliente
            var clienteId = await _context.Cliente
                .Where(c => c.CuitCuil == cuitCuil)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(); // Devuelve si encontró o sino default

            if (clienteId == default)
            {
                return new List<ClienteDtoOut>(); // No está el cliente, devuelve lista vacía
            }

            // Si se encontró el cliente, selecciono y mapeo sus datos
            var cliente = await _context.Cliente
                .Where(c => c.Id == clienteId)
                .Select(c => new ClienteDtoOut
                {
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    CuitCuil = c.CuitCuil,
                    Alta = c.Alta,
                    Calle = c.Calle,
                    Departamento = c.Departamento,
                    AlturaCalle = c.AlturaCalle,
                    Username = c.Username,
                    Localidad = c.Localidad.Nombre
                }).ToListAsync();

            return cliente;
        }

        public async Task<ClienteIdDtoOut> GetIdByCuitCuil(string cuitCuil)
        {
            var clienteId= await _context.Cliente
                .Where(c => c.CuitCuil== cuitCuil)
                .Select(c => new ClienteIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return clienteId;
        }

    }
}

