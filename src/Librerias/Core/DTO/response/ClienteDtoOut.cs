using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteDtoOut
    {
        public required string RazonSocial { get; set; }
        public required string CuitCuil { get; set; } 
        public required DateOnly Alta { get; set; }
        public required string Calle { get; set; }
        public string? Departamento { get; set; }
        public required string Numero { get; set; }

    }
}
