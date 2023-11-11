using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string CuitCuil { get; set; } = null!;

    public int Legajo { get; set; }

    public int? SucursalId { get; set; }

    public virtual Sucursal? Sucursal { get; set; }
}
