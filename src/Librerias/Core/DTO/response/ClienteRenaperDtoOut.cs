using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.response
{
    public class ClienteRenaperDtoOut
    {
        //public required string datos {  get; set; }
        public required bool exito { get; set; }
        public required string mensaje { get; set;}
        // DATOS
            public string Nombre { get; set; }
            public string Rol { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Cuil { get; set; }
            public bool Estado { get; set; }
            public bool EstadoCrediticio { get; set; }
            /*public int nbf { get; set; }
            public int exp { get; set; }
            public string iss { get; set; }
            public string aud { get; set; }*/
        
    }
    public class RespuestaInterna<T>
    {
        public T datos { get; set;}
    }
}
