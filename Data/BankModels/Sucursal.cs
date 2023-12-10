using System;
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

    public int? LocalidadId { get; set; }

    public virtual ICollection<Cuenta>? Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Localidad? Localidad { get; set; }
}
