using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class ClienteDtoIn
    {
        public required int Id { get; set; }
        public required string RazonSocial { get; set; }
        public required string CuitCuil { get; set; } 
        public required DateTime Alta { get; set; }
        public required string Calle { get; set; }
        public string? Departamento { get; set; }
        public required string AlturaCalle { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public required int IdLocalidad { get; set; }
    }
}
