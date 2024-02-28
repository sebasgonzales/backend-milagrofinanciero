using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteRenaperDtoOut
    {
        public required string datos {  get; set; } 
        public required bool exito { get; set; }
        public required string mensaje { get; set;}
    }

}
