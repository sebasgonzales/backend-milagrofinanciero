namespace backend_milagrofinanciero.Data.DTOS.request
{
    public class EmpleadoDtoIn
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string CuitCuil { get; set; } = null!;

        public int Legajo { get; set; }
        public int SucursalId {  get; set; }
    }
}
