using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class LoginService : ILoginService
    {
        private readonly milagrofinancierog1Context _context;

        public LoginService(milagrofinancierog1Context context)
        {
            _context = context;
        }

        public async Task<ClienteDtoOut?> AuthenticateCliente(string username, string password)
        {
            // Buscar el cliente por nombre de usuario y contraseña
            return await _context.Cliente.
                Where(c => c.Username == username && c.Password == password)
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
            ;
            
        }

        public async Task<string?> LoginAndGetCuitCuil(string username, string password)
        {
            // Buscar el cliente por nombre de usuario y contraseña
            var client = await _context.Cliente.FirstOrDefaultAsync(c => c.Username == username && c.Password == password);

            // Si el cliente es nulo, no se encontró ningún cliente con el usuario y contraseña proporcionados
            if (client == null)
            {
                return null;
            }

            // Si se encontró un cliente, devolver su ID
            return client.CuitCuil;
        }

    }
}
