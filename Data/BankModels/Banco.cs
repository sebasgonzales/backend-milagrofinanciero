using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Banco
{
    public int Id { get; set; }

    public string[] Nombre { get; set; } = null!;

    public virtual Cuenta? Cuentum { get; set; }
}
