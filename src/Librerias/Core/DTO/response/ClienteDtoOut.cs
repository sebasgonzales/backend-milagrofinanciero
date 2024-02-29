using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteDtoOut
    {
        public string Username { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }

        public required string CuitCuil { get; set; }
        public required DateTime Alta { get; set; }
        public required string Calle { get; set; }
        public string? Departamento { get; set; }
        public required string AlturaCalle { get; set; }
        public required string Localidad { get; set; }
    }
    public class ClienteIdDtoOut
    {
        public required int Id { get; set; }
    }
    public class ClienteRenaper
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Cuil { get; set; }
        public bool Estado { get; set; }
        public bool EstadoCrediticio { get; set; }
        /*public int nbf { get; set; }
        public int exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }*/
    }
}