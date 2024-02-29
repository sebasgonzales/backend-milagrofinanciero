namespace Core.DTO.request
{
    public class TransaccionExternaDtoIn
    {
        public required int Monto { get; set; }
        public DateTime Realizacion { get; set; }
        public int IdTipoMotivo { get; set; }
        public string Referencia { get; set; }
        public required string CbuCuentaOrigen { get; set; }
        public required string CbuCuentaDestino { get; set; }
        public int IdTipoTransaccion { get; set; }
    }

}
