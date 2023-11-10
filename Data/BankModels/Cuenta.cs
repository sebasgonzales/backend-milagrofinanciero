using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Cuenta
{
    public int Id { get; set; }

    public long NumeroCuenta { get; set; }

    public long Cbu { get; set; }

    public virtual ClienteXCuenta? ClienteXCuenta{ get; set; }

    public virtual TipoCuenta? TipoCuenta { get; set; } 

    public virtual Banco? Banco { get; set; }

    public virtual Transaccion? Transaccion { get; set; }
}
