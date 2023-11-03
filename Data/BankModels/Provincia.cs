using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Provincia
{
    public int Id { get; set; }

    public string[] Nombre { get; set; } = null!;

    public virtual Pais IdNavigation { get; set; } = null!;

    public virtual Localidad? Localidad { get; set; }

    public virtual Sucursal? Sucursal { get; set; }
}
