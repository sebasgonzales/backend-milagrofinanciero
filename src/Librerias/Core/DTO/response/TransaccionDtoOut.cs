using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class TransaccionDtoOut
    {
        public required float Monto { get; set; }
        //public required long Numero { get; set; }
        public required DateOnly Acreditacion { get; set; }
        public required DateOnly Realizacion { get; set; }
        public required string Motivo { get; set; }
        public string? Referencia { get; set; }
        public required long CuentaDestino { get; set; }
        public required long CuentaOrigen { get; set; }
        public required string TipoTransaccion { get; set; }
    }
}
