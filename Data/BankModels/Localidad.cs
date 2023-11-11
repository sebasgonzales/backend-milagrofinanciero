using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Localidad
{
    public int Id { get; set; }

    public string Cp { get; set; } = null!;

    public int ProvinciaId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Provincia Provincia { get; set; } = null!;

    public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
}
