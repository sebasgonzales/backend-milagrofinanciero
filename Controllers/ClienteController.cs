using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]


    
public class ClienteController : ControllerBase
    
{
        private readonly IClienteService _service;
        public ClienteController(IClienteService cliente)
        { _service = cliente; }
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

    //clienteId es un parametro dinamico en la ruta
    [HttpGet("clientes/CuitCuil/{cuitCuil}/ClienteXCuenta")] // Modificación en la ruta del endpoint
    public async Task<ActionResult<CuentaDtoOut>> GetCuentasByCuitCuil(string cuitCuil) // Modificación en el nombre del método
    {
        var cuentas = await _service.GetCuentasByCuitCuil(cuitCuil);

        if (cuentas == null || !cuentas.Any()) // Modificación en la condición
        {
            return NotFound();
        }

        return Ok(cuentas);
    }

}



