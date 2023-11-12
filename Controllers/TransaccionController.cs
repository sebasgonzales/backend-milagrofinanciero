using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransaccionController: ControllerBase
    {
        private readonly TransaccionService _service;
        public TransaccionController(TransaccionService transaccion)
        {
            _service = transaccion;
        }

        [HttpGet]
        public IEnumerable<Transaccion> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Transaccion> GetById(int id)
        {
            var transaccion = _service.GetById(id);
        
            if (transaccion is null)
                    return NotFound();

            return transaccion;
        }

        [HttpPost]
        public IActionResult Create(Transaccion transaccion) 
        {
            var newTransaccion = _service.Create(transaccion);

            return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, Transaccion transaccion)
        {

            if (id != transaccion.Id)
                return BadRequest();

            var transaccionToUpdate = _service.GetById(id);

            if (transaccionToUpdate is not null)
            {
                _service.Update(id,transaccion);
                return NoContent();
            }
            else 
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaccionToDelete= _service.GetById(id);

            if (transaccionToDelete is not null)
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
