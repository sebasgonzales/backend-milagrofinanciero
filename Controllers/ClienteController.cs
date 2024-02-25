using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IEnumerable<ClienteDtoOut>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [HttpGet("clientes/CuitCuil/{cuitCuil}/ClienteCuenta")] // Modificación en la ruta del endpoint
    [Authorize]
    public async Task<ActionResult<CuentaDtoOut>> GetCuentasByCuitCuil(string cuitCuil)
    {
        var cuentas = await _service.GetCuentasByCuitCuil(cuitCuil);

        if (cuentas == null)
        {
            return NotFound();
        }

        return Ok(cuentas);
    }

    [HttpGet("clientes/Nombre/{cuitCuil}/Cliente")]
    [Authorize]
    public async Task<ActionResult<string>> GetNombreByCuitCuil(string cuitCuil)
    {
        var cliente = await _service.GetNombre(cuitCuil);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    //clienteId es un parametro dinamico en la ruta
    [HttpGet("cliente/{cuitCuil}")] // Modificación en la ruta del endpoint
    [Authorize]
    public async Task<ActionResult<ClienteDtoOut>> GetClienteByCuitCuil(string cuitCuil)
    {
        var cliente = await _service.GetClienteByCuitCuil(cuitCuil);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    [HttpGet("IdxCuitCuil/{cuitCuil}")]
    //no autorizo se usa en el registrar
    public async Task<ActionResult<ClienteIdDtoOut>> GetIdByCuitCuil(string cuitCuil)
    {
        try
        {
            var clienteId = await _service.GetIdByCuitCuil(cuitCuil);

            if (clienteId is null)
            {
                return NotFound("No se encontró un cliente con el cuitcuil proporcionado.");
            }
            return clienteId;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}



