using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Cuenta
{
    public int Id { get; set; }

    public long[] NumeroCuenta { get; set; } = null!;

    public long[] Cbu { get; set; } = null!;

    public virtual ClienteXCuenta? ClienteXcuentum { get; set; }

    public virtual TipoCuenta Id1 { get; set; } = null!;

    public virtual Banco IdNavigation { get; set; } = null!;

    public virtual Transaccion? Transaccion { get; set; }
}
