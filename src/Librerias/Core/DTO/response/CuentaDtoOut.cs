﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class CuentaDtoOut
    {
        public required long NumeroCuenta { get; set; }
        public required long Cbu { get; set; }
        public required string TipoCuenta { get; set; }
        public required string Banco { get; set; }
        public required string Sucursal { get; set; }
    }
}
