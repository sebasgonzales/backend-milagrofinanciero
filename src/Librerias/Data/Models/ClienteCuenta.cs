﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ClienteCuenta
{
    public int Id { get; set; }

    public DateTime Alta { get; set; }

    public int IdCliente { get; set; }

    public int IdCuenta { get; set; }
    public bool Titular { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual Cuenta Cuenta { get; set; }
}