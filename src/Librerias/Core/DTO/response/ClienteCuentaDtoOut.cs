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
        public required DateOnly Alta { get; set; }
        public required string Cliente { get; set; } 
        public required long Cuenta { get; set; }
    }
}
