﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;


namespace Data.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string RazonSocial { get; set; }

    public string CuitCuil { get; set; }

    public DateTime Alta { get; set; }

    public string Calle { get; set; }

    public string Departamento { get; set; }

    public string AlturaCalle { get; set; }

    public int IdLocalidad { get; set; }
    public string Username { get; set; }

    public string Password { get; set; }

    /*public string _password
    {
        get { return Password; }
        set { Password = Hashing.HashearConSHA256(value); }
    }*/

    public virtual ICollection<ClienteXcuenta> ClienteXcuenta { get; set; } = new List<ClienteXcuenta>();

    public virtual Localidad Localidad { get; set; }
}