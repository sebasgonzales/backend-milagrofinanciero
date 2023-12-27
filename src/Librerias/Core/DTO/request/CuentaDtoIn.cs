using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class CuentaDtoIn
    {
        public required int Id { get; set; }
        public required long Numero { get; set; }
        public required long Cbu { get; set; }
        public required int IdTipoCuenta { get; set; }
        public required int IdBanco { get; set; }
        public required int IdSucursal { get; set; }
    }
}
