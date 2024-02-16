using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteCuentaDtoOut
    {
        public required bool Titular { get; set; } 
        public required DateTime Alta { get; set; }
        public required string Cliente { get; set; } 
        public required long Cuenta { get; set; }
    }
    public class CuentaTitularOrNotDtoOut
    {
        public required bool Titular { get; set; }
    }
}
