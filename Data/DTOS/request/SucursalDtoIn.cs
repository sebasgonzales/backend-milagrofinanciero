using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.request;


public class SucursalDtoIn
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string? Departamento { get; set; }

    public string Numero { get; set; } = null!;

    public int CuentaId { get; set; }

    public int LocalidadId { get; set; }

}