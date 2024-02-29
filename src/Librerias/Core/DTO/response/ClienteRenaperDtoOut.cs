using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteRenaperDtoOut
    {
        public required string datos {  get; set; }
        public required bool exito { get; set; }
        public required string mensaje { get; set;}

            /*public int nbf { get; set; }
            public int exp { get; set; }
            public string iss { get; set; }
            public string aud { get; set; }*/
        
    }
    public class RespuestaInterna<T>
    {
        public T Datos { get; set; }
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = "";
    }
}

