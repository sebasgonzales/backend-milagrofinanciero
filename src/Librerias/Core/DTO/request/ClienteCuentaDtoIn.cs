using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class ClienteCuentaDtoIn
    {
        public required int Id { get; set; }
        public required bool Titular { get; set; }
        public required DateTime Alta { get; set; }
        public required int IdCliente { get; set; }
        public required int IdCuenta { get; set; }
    }
}