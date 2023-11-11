using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Localidad
{
    public int Id { get; set; }

    public string Cp { get; set; } = null!;

    public virtual Cliente? Cliente { get; set; }

    public virtual Provincia Provincia { get; set; } = null!;
}
