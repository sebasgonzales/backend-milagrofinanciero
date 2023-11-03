using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Empleado
{
    public int Id { get; set; }

    public string[] Nombre { get; set; } = null!;

    public long[] CuitCuil { get; set; } = null!;

    public int Legajo { get; set; }

    public virtual Sucursal IdNavigation { get; set; } = null!;
}
