using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        public async Task<IEnumerable<Transaccion>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccion>> GetById(int id)
        {
            var transaccion = await _service.GetById(id);
        
            if (transaccion is null)
                    return TransaccionNotFound(id);

            return transaccion;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaccion transaccion) 
        {
            var newTransaccion = await _service.Create(transaccion);

            return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Transaccion transaccion)
        {

            if (id != transaccion.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({transaccion.Id}) del cuerpo de la solicitud.  " });

            var transaccionToUpdate = await _service.GetById(id);

            if (transaccionToUpdate is not null)
            {
                await _service.Update(id,transaccion);
                return NoContent();
            }
            else 
            {
                return TransaccionNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaccionToDelete= await _service.GetById(id);

            if (transaccionToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return TransaccionNotFound(id);
            }
        }

        public NotFoundObjectResult TransaccionNotFound(int id)
        {
            return NotFound(new { message = $"La transaccion con ID = {id} no existe. " });
        }
    }
}
