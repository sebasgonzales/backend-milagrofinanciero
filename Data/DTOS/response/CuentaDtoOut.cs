using backend_milagrofinanciero.Data.BankModels;
namespace backend_milagrofinanciero.Data.DTOS.response
{
    public class CuentaDtoOut
    {
        public long NumeroCuenta { get; set;}
        public long Cbu { get;  set;}
        public string TipoCuenta { get; set;}
        public string Banco { get; set;}
        public string Sucursal {  get; set;}
    }
}
