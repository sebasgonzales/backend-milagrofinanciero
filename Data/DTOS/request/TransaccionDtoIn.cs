namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class TransaccionDtoIn
    {
        public int Id { get; set; }

        public float Monto { get; set; }
        public long NumeroOperacion { get; set; }

        public DateOnly? Acreditacion { get; set; }

        public DateOnly? Realizacion { get; set; }

        public string Motivo { get; set; } = null!;

        public string? Referencia { get; set; }

        public int CuentaOrigenId { get; set; }

        public int CuentaDestinoId { get; set; }

        public int TipoTransaccionId { get; set; }
    }
}
