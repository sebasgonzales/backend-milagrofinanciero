using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class SucursalDtoOut
    {
        public required string Nombre { get; set; }
        public required string Calle { get; set; } 
        public string? Departamento { get; set; }
        public required string Numero { get; set; } 
        public required string Localidad { get; set; } 

    }
}
