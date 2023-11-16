using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.request;


public class ClienteDtoIn
{
    public int Id { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string CuitCuil { get; set; } = null!;

    public DateOnly? Alta { get; set; }

    public string? Calle { get; set; }

    public string? Departamento { get; set; }

    public string? Numero { get; set; }

    public int LocalidadId { get; set; }


}
