namespace backend_milagrofinanciero.Data.DTOS.response
{
    public class EmpleadoDtoOut
    {
        public string Nombre { get; set; } = null!;

        public string CuitCuil { get; set; } = null!;

        public int Legajo { get; set; }

        public string? SucursalNombre { get; set; }
    }
}
