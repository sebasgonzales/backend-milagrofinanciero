using backend_milagrofinanciero.Data.BankModels;
using System.Numerics;

namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class CuentaDtoIn
    {
        public int idCuenta {  get; set; }
        public long numeroCuenta {  get; set; }
        public long cbu {  get; set; }
        public TipoCuenta? TipoCuenta { get; set; } = null;
       // public Banco? Banco { get; set; }

    }
}
