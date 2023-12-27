using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteXCuentaController : ControllerBase
    {
        private readonly IClienteXCuentaService _service;
        public ClienteXCuentaController(IClienteXCuentaService clienteXCuenta)
        {
            _service = clienteXCuenta;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteXCuentaDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteXCuentaDtoOut>> GetById(int id)
        {
            var clienteXCuenta = await _service.GetDtoById(id);

            if (clienteXCuenta is null)
                return ClienteXCuentaNotFound(id);

            return clienteXCuenta;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteXCuentaDtoIn clienteXCuenta)
        {

            var newClienteXCuenta = await _service.Create(clienteXCuenta);

            return CreatedAtAction(nameof(GetById), new { id = newClienteXCuenta.Id }, newClienteXCuenta);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, ClienteXCuentaDtoIn clienteXCuenta)
        {

            if (id != clienteXCuenta.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({clienteXCuenta.Id}) del cuerpo de la solicitud.  " });

            var clienteXCuentaToUpdate = await _service.GetById(id);

            if (clienteXCuentaToUpdate is not null)
            {
                await _service.Update(id, clienteXCuenta);
                return NoContent();
            }
            else
            {
                return ClienteXCuentaNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteXCuentaToDelete = await _service.GetById(id);

            if (clienteXCuentaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return ClienteXCuentaNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult ClienteXCuentaNotFound(int id)
        {
            return NotFound(new { message = $"el ClienteXCuenta con ID = {id} no existe. " });
        }

    }
}
