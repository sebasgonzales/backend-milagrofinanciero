using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransaccionController: ControllerBase
    {
        private readonly ITransaccionService _service;
        public TransaccionController(ITransaccionService transaccion)
        {
            _service = transaccion;
        }

        [HttpGet]
        public async Task<IEnumerable<TransaccionDtoOut>> Get()
        {
            return await _service.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TransaccionDtoOut>> GetById(int id)
        {
            var transaccion = await _service.GetDtoById(id);
        
            if (transaccion is null)
                    return TransaccionNotFound(id);

            return transaccion;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(TransaccionDtoIn transaccion) 
        //{
        //    var newTransaccion = await _service.Create(transaccion);

        //    return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
        //}

        [HttpPost]

        public async Task<IActionResult> Crear(TransaccionDtoIn transaccion, long cbu, float monto)
        {
            var saldoDisponible = await _service.GetSaldo(cbu, monto);

            if (saldoDisponible == 1)
            {
                var newTransaccion = await _service.Create(transaccion);

                return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
            }
            else
            {
                return Error();
            }
        }
        //f

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, TransaccionDtoIn transaccion)
        {

            if (id != transaccion.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({transaccion.Id}) del cuerpo de la solicitud.  " });

            var transaccionToUpdate = await _service.GetById(id);

            if (transaccionToUpdate is not null)
            {
                await _service.Update(transaccion);
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

        [NonAction]
        public NotFoundObjectResult TransaccionNotFound(int id)
        {
            return NotFound(new { message = $"La transaccion con ID = {id} no existe. " });
        }

        [NonAction]
        public NotFoundObjectResult Error()
        {
            return NotFound(new { message = "No tiene saldo suficiente" });
        }
    }
}
