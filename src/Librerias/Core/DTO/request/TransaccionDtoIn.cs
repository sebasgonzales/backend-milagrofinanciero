using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class TransaccionDtoIn
    {
        public required int Id { get; set; }
        public required float Monto { get; set; }
        //public required long Numero { get; set; }
        public required DateTime Acreditacion { get; set; }
        public required DateTime Realizacion { get; set; }
        public required int idTipoMotivo { get; set; }
        public string? Referencia { get; set; }
        public required int IdCuentaOrigen { get; set; }
        public required int IdCuentaDestino { get; set; }
        public required int IdTipoTransaccion { get; set; }

    }
    public class TransaccionIdDtoIn
    {
        public required int Id { get; set;}
    }
}
