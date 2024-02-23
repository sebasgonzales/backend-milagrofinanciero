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
    public class ContactoService : IContactoService
    {

        private readonly milagrofinancierog1Context _context;
        public ContactoService(milagrofinancierog1Context context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ContactoDtoOut>> GetAll()
        {
            return await _context.Contacto.Select(c => new ContactoDtoOut
            {
                Nombre = c.Nombre,
                Cbu = c.Cbu,
                Banco = c.Banco.Nombre,
                Cuenta = c.Cuenta.Numero
            }).ToListAsync();

        }

        public async Task<ContactoDtoOut?> GetDtoById(int id)
        {
            return await _context.Contacto
                .Where(c => c.Id == id)
                .Select(c => new ContactoDtoOut
                {
                    Nombre = c.Nombre,
                    Cbu = c.Cbu,
                    Banco = c.Banco.Nombre,
                    Cuenta = c.Cuenta.Numero
                }).SingleOrDefaultAsync();

        }

        public async Task<Contacto?> GetById(int id)
        {
            return await _context.Contacto.FindAsync(id);
        }

        public async Task<Contacto> Create(ContactoDtoIn newContactoDto)
        {
            var newContacto = new Contacto();

            newContacto.Nombre = newContactoDto.Nombre;
            newContacto.Cbu = newContactoDto.Cbu;
            newContacto.IdBanco = newContactoDto.IdBanco;
            newContacto.IdCuenta = newContactoDto.IdCuenta;

            _context.Contacto.Add(newContacto);
            await _context.SaveChangesAsync();

            return newContacto;

        }

        public async Task Update(int id, ContactoDtoIn contacto)
        {
            var existingContacto = await GetById(id);

            if (existingContacto is not null)
            {

                existingContacto.Nombre = contacto.Nombre;
                existingContacto.Cbu = existingContacto.Cbu;
                existingContacto.IdBanco = existingContacto.IdBanco;
                existingContacto.IdCuenta = existingContacto.IdCuenta;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var contactoToDelete = await GetById(id);

            if (contactoToDelete is not null)
            {

                _context.Contacto.Remove(contactoToDelete);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ContactoIdDtoOut> GetIdByCbu(string cbu)
        {
            var contacto = await _context.Contacto
                .Where(c => c.Cbu == cbu)
                .Select(c => new ContactoIdDtoOut { Id = c.Id })
                .SingleOrDefaultAsync();

            return contacto;
        }

    }
}

