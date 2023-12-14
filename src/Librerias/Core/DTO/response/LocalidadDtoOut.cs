using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class LocalidadDtoOut
    {
        public required string Nombre { get; set; }
        public required string CodigoPostal { get; set; }
        public required string NombreProvincia { get; set; }
    }
}
