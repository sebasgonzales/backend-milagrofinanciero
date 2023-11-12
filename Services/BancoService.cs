using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;


namespace backend_milagrofinanciero.Services
{
    public class BancoService
    {

        private readonly MilagrofinancieroG1Context _context;
        public BancoService(MilagrofinancieroG1Context context)
        {
            _context = context;

        }

        public IEnumerable<Banco> GetAll()
        {
            return _context.Bancos.ToList();

        }

        public Banco? GetById(int id)
        {
            return _context.Bancos.Find(id);
        }

        public Banco Create(Banco newbanco)
        {
            _context.Bancos.Add(newbanco);
            _context.SaveChanges();

            return newbanco;

        }

        public void Update(int id, Banco banco)
        {
            var existingBanco = GetById(id);

            if (existingBanco is not null)
            {
               
                existingBanco.Nombre = banco.Nombre;
                _context.SaveChanges();
            }
          
        }

        public void Delete(int id)
        {
            var bancoToDelete = GetById(id);

            if (bancoToDelete is not null)
            {

                _context.Bancos.Remove(bancoToDelete);
                _context.SaveChanges();
            }

        }


    }
}
