﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TipoMotivo
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Transaccion> Transaccion { get; set; } = new List<Transaccion>();
}