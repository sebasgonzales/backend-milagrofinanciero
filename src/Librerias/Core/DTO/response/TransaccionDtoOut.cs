﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class TransaccionDtoOut
    {
        public required float Monto { get; set; }
        
        public required long Numero { get; set; }
        public required DateTime Realizacion { get; set; }
        public required string Motivo { get; set; }
        public string? Referencia { get; set; }
        public required string CbuDestino { get; set; }
        public required string CbuOrigen { get; set; }
        public required string TipoTransaccion { get; set; }
    }

   
}
