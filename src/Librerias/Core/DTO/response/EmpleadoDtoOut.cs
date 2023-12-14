using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class EmpleadoDtoOut
    {
        public required string Nombre { get; set; }
        public required string CuitCuil { get; set; }
        public required int Legajo { get; set; }
        public required string SucursalNombre { get; set; }
    }
}
