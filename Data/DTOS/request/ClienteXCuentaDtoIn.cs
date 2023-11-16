using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class ClienteXCuentaDtoIn
    {
        public int Id { get; set; }

        public string Rol { get; set; } = null!;

        public DateOnly? Alta { get; set; }

        public int ClienteId { get; set; }

        public int CuentaId { get; set; }
        
    }
}
