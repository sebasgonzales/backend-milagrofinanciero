using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Contacto
    {
        public int Id { get; set; }

        public string Cbu { get; set; }

        public string Nombre {  get; set; }

        public int IdCuenta { get; set; }

        public int IdBanco { get; set; }

        public virtual Banco Banco { get; set; }

        public virtual Cuenta Cuenta { get; set; }
    }
}
