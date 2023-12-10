using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Cuenta
{
    public int Id { get; set; }

    public long NumeroCuenta { get; set; }

    public long Cbu { get; set; }

    public int TipoCuentaId { get; set; }

    public int BancoId { get; set; }
    public int SucursalId { get; set; }

    public virtual Banco Banco { get; set; } = null!;
    public virtual Sucursal Sucursal { get; set; } = null!;

    public virtual ICollection<ClienteXCuenta> ClienteXCuenta { get; set; } = new List<ClienteXCuenta>();

    public virtual TipoCuenta TipoCuenta { get; set; } = null!;

    public virtual ICollection<Transaccion> TransaccionCuentaDestinos { get; set; } = new List<Transaccion>();

    public virtual ICollection<Transaccion> TransaccionCuentaOrigens { get; set; } = new List<Transaccion>();
}
