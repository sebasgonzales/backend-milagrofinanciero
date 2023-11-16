namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class LocalidadDtoIn
    {
        public int Id { get; set; }
        public string CodigoPostal { get; set; }
        public int ProvinciaId { get; set; }
    }
}
