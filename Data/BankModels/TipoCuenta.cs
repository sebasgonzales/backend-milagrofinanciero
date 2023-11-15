using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend_milagrofinanciero.Data.BankModels;

public partial class TipoCuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly? Alta { get; set; }


    //propiedad de navegacion
    [JsonIgnore]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}


