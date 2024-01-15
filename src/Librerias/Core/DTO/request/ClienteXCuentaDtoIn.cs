using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class ClienteXCuentaDtoIn
    {
        public required int Id { get; set; }
        public required string Rol { get; set; }
        public required DateOnly Alta { get; set; }
        public required int IdCliente { get; set; }
        public required int IdCuenta { get; set; }
    }
}