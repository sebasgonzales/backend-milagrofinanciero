using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class ClienteXCuenta
{
    public int Id { get; set; }

    public string Rol { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public int ClienteId { get; set; }

    public int CuentaId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Cuenta Cuenta { get; set; } = null!;
}
