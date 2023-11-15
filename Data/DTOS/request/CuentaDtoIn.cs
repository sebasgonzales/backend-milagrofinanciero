using backend_milagrofinanciero.Data.BankModels;
using System.Numerics;

namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class CuentaDtoIn
    {
        public int id {  get; set; }
        public long NumeroCuenta {  get; set; }
        public long Cbu {  get; set; }
        public TipoCuenta? TipoCuenta { get; set; } = null;
        public Banco? Banco { get; set; }

    }
}
