using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]



    public class BancoController : ControllerBase
    {
        private readonly BancoService _service;

        public BancoController(BancoService banco)
        {
            _service = banco;

        }


        [HttpGet]
        public IEnumerable<Banco> Get()
        {
            return _service.GetAll();

        }


        [HttpGet("{id}")]
        public ActionResult<Banco> GetById(int id)
        {
            var banco = _service.GetById(id);

            if (banco is null)
                return NotFound();
            return banco;
        }


        [HttpPost]

        public IActionResult Create(Banco banco)
        {
            var newBanco = _service.Create(banco);


            return CreatedAtAction(nameof(GetById), new { id = newBanco.Id}, newBanco);
        
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Banco banco)
        {
            if (id != banco.Id)
                return BadRequest();

            var bancoToUpdate = _service.GetById(id);

            if (bancoToUpdate is not null) 
            {
                _service.Update(id, banco);
                return NoContent();
            
            }else
            {
                return NotFound();

            }
           
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var bancoToDelete = _service.GetById(id);

            if (bancoToDelete is not null)
            {
                _service.Delete(id);
                return Ok();

            }
            else
            {
                return NotFound();

            }

        }



    }
}
