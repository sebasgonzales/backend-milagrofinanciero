using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Sucursal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public virtual Empleado? Empleado { get; set; }

    public virtual Provincia Provincia { get; set; } = null!;
}
