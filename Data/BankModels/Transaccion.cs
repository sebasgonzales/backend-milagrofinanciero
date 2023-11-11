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

    public int CuentaOrigenId { get; set; }

    public int CuentaDestinoId { get; set; }

    public int TipoTransaccionId { get; set; }

    public virtual Cuenta CuentaDestino { get; set; } = null!;

    public virtual Cuenta CuentaOrigen { get; set; } = null!;

    public virtual TipoTransaccion TipoTransaccion { get; set; } = null!;
}
