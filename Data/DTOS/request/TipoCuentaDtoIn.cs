namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class TipoCuentaDtoIn
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public DateOnly? Alta { get; set; }

    }
}
