using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Transaccion
{
    public int Id { get; set; }

    public float Monto { get; set; }

    public long NumeroOperacion { get; set; }

    public DateOnly? Acreditacion { get; set; }

    public DateOnly? Realizacion { get; set; }

    public string Motivo { get; set; } = null!;

    public string? Referencia { get; set; }

    public virtual TipoTransaccion TipoTransaccion { get; set; } = null!;

    public virtual Cuenta Cuenta { get; set; } = null!;
}
