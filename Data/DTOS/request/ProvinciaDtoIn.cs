namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class ProvinciaDtoIn
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int PaisId { get; set; }
    }
}
