using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.response;


public class SucursalDtoOut
{

    public string Nombre { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string? Departamento { get; set; }

    public string Numero { get; set; } = null!;

    public string Localidad { get; set; } = null!;

}