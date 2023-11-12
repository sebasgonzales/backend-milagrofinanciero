using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]



    public class BancoController : ControllerBase
    {
        private readonly MilagrofinancieroG1Context _context;

        public BancoController(MilagrofinancieroG1Context context)
        {
            _context = context;

        }


        [HttpGet]
        public IEnumerable<Banco> Get()
        {
            return _context.Bancos.ToList();

        }


        [HttpGet("{id}")]
        public ActionResult<Banco> GetById(int id)
        {
            var banco = _context.Bancos.Find(id);

            if (banco is null)
                return NotFound();
            return banco;
        }


        [HttpPost]

        public IActionResult Create(Banco banco)
        {
            _context.Bancos.Add(banco);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = banco.Id}, banco);
        
        }

        [HttpPut("{id}")]
        public IActionResult Uptade(int id, Banco banco)
        {
            if (id != banco.Id)
                return BadRequest();

            var existingBanco = _context.Bancos.Find(id);
            if (existingBanco is null)
                return NotFound();
            existingBanco.Nombre = banco.Nombre;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingBanco = _context.Bancos.Find(id);
            if (existingBanco is null)
                return NotFound();

            _context.Bancos.Remove(existingBanco);
            _context.SaveChanges();

            return Ok();


        }



    }
}
