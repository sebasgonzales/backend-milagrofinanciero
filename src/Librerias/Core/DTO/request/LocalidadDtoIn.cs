using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class LocalidadDtoIn
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string CodigoPostal { get; set; }
        public required int IdProvincia { get; set; }
    }
}
