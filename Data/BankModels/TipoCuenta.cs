using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class TipoCuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public virtual Cuenta? Cuentum { get; set; }
}
