﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class TipoCuentaDtoIn
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateOnly Alta { get; set; }
    }
}
