using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ContactoDtoOut
    {
        public required string Nombre { get; set; }
        public required string Cbu { get; set; }
        public string Banco { get; set;}

        public long Cuenta { get; set; }

    }

    public class ContactoIdDtoOut
    {
        public required int Id { get; set; }
    }
}
