﻿using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Sucursal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string? Departamento { get; set; }

    public string Numero { get; set; } = null!;

    public int? CuentaId { get; set; }

    public int? LocalidadId { get; set; }

    public virtual Cuenta? Cuenta { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Localidad? Localidad { get; set; }
}
