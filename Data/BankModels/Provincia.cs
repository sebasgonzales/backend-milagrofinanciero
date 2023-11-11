using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Provincia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int PaisId { get; set; }

    public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();

    public virtual Pais Pais { get; set; } = null!;
}
