using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class ContactoDtoIn
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required long Cbu { get; set; }
        public int IdBanco { get; set; }
        public int IdCuenta {  get; set; }
    }
}
