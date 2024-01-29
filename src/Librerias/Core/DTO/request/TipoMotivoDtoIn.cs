using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class TipoMotivoDtoIn
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
    }
}
