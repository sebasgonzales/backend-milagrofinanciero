﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class BancoDtoOut
    {
        public required string Nombre { get; set; }
        public required string Codigo { get; set; }

    }
    public class BancoIdDtoOut
    {
        public required int Id { get; set; }
    }

}
