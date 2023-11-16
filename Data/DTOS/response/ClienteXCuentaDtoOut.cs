using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.response
{
    public class ClienteXCuentaDtoOut
    {
        public string Rol { get; set; } = null!;

        public DateOnly? Alta { get; set; }

        public string Cliente  { get; set; } = null!;

        public long Cuenta { get; set; } 
    }
}
