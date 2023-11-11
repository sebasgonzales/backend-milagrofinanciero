using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class ClienteXCuenta
{
    public int Id { get; set; }

    public string Rol { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;
}
