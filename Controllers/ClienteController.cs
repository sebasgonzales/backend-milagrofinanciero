using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Controllers;

    [ApiController]
[Route("[controller]")]


    
public class ClienteController : ControllerBase
    
{
        private readonly ClienteService _service;
        public ClienteController(ClienteService service)
        { _service = service; }
        [HttpGet]
        public async Task<IEnumerable<ClienteDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDtoOut>>? GetById(int id)
        {
        var cliente = await _service.GetByIdDto(id);

        if (cliente is null)
            return NotFound(id);

        return cliente;
        }

    [HttpPost]
    public async Task<IActionResult> Create(ClienteDtoIn cliente)
    {
        //creando un nuevo objeto
        var newCliente = await _service.Create(cliente);

        return CreatedAtAction(nameof(GetById), new { id = newCliente.Id }, newCliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClienteDtoIn cliente)
    {
        if (id != cliente.Id)
            return BadRequest();

        var clienteToUpdate = await _service.GetById(id);

        if (clienteToUpdate is not null) 
        { await _service.Update(id, cliente);
            return NoContent();
        }
        else
        { return NotFound(); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteToDelete = await _service.GetById(id);

        if (clienteToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        { return NotFound(); }
    }
}