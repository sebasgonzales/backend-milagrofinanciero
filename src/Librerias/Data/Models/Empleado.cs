﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string CuitCuil { get; set; }

    public int Legajo { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Sucursal Sucursal { get; set; }
}