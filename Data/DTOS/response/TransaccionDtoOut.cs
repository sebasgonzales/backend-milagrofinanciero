using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data.DTOS.response
{
    public class TransaccionDtoOut
    { 
        public float Monto { get; set; }

        public long NumeroOperacion { get; set; }

        public DateOnly? Acreditacion { get; set; }

        public DateOnly? Realizacion { get; set; }

        public string Motivo { get; set; } = null!;

        public string? Referencia { get; set; }

        public long  CuentaDestino { get; set; } 

        public long  CuentaOrigen { get; set; } 

        public string  TipoTransaccion { get; set; } = null!;
    }
}
