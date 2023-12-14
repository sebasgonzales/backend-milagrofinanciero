﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public long Numero { get; set; }

    public long Cbu { get; set; }

    public int IdTipoCuenta { get; set; }

    public int IdBanco { get; set; }

    public int IdSucursal { get; set; }

    public virtual ICollection<ClienteXcuenta> ClienteXcuenta { get; set; } = new List<ClienteXcuenta>();

    public virtual Banco IdBancoNavigation { get; set; }

    public virtual Sucursal IdSucursalNavigation { get; set; }

    public virtual TipoCuenta IdTipoCuentaNavigation { get; set; }

    public virtual ICollection<Transaccion> TransaccionIdCuentaDestinoNavigation { get; set; } = new List<Transaccion>();

    public virtual ICollection<Transaccion> TransaccionIdCuentaOrigenNavigation { get; set; } = new List<Transaccion>();
}