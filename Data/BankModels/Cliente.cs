﻿using System;
using System.Collections.Generic;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class Cliente
{
    public int Id { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string CuitCuil { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public string? Calle { get; set; }

    public string? Departamento { get; set; }

    public string? Numero { get; set; }

    public int LocalidadId { get; set; }

    public virtual ICollection<ClienteXCuenta> ClienteXCuenta { get; set; } = new List<ClienteXCuenta>();

    public virtual Localidad Localidad { get; set; } = null!;
}
