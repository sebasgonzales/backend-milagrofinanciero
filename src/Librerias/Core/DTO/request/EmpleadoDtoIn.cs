using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class EmpleadoDtoIn
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; } 
        public required string CuitCuil { get; set; } 
        public required int Legajo { get; set; }
        public required int IdSucursal { get; set; }
    }
}
