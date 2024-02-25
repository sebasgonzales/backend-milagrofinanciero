using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ClienteCuentaController : ControllerBase
    {
        private readonly IClienteCuentaService _service;
        public ClienteCuentaController(IClienteCuentaService clienteCuenta)
        {
            _service = clienteCuenta;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ClienteCuentaDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteCuentaDtoOut>> GetById(int id)
        {
            var clienteCuenta = await _service.GetDtoById(id);

            if (clienteCuenta is null)
                return ClienteCuentaNotFound(id);

            return clienteCuenta;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteCuentaDtoIn clienteCuenta)
        {

            var newClienteCuenta = await _service.Create(clienteCuenta);

            return CreatedAtAction(nameof(GetById), new { id = newClienteCuenta.Id }, newClienteCuenta);
        }

        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, ClienteCuentaDtoIn clienteCuenta)
        {

            if (id != clienteCuenta.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({clienteCuenta.Id}) del cuerpo de la solicitud.  " });

            var clienteCuentaToUpdate = await _service.GetById(id);

            if (clienteCuentaToUpdate is not null)
            {
                await _service.Update(id, clienteCuenta);
                return NoContent();
            }
            else
            {
                return ClienteCuentaNotFound(id);
            }

        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteCuentaToDelete = await _service.GetById(id);

            if (clienteCuentaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return ClienteCuentaNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult ClienteCuentaNotFound(int id)
        {
            return NotFound(new { message = $"el ClienteCuenta con ID = {id} no existe. " });
        }

    }
}
