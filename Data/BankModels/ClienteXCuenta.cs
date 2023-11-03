using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class ClienteXCuenta
{
    public int Id { get; set; }

    public string[] Rol { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public virtual Cuenta Id1 { get; set; } = null!;

    public virtual Cliente IdNavigation { get; set; } = null!;
}
