﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Localidad
{
    public int Id { get; set; }

    public string CodigoPostal { get; set; }

    public int IdProvincia { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();

    public virtual Provincia IdProvinciaNavigation { get; set; }

    public virtual ICollection<Sucursal> Sucursal { get; set; } = new List<Sucursal>();
}