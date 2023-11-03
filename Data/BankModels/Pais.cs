using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Pais
{
    public int Id { get; set; }

    public string[] Nombre { get; set; } = null!;

    public virtual Provincia? Provincium { get; set; }
}
