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



public class SucursalController : ControllerBase

{
    private readonly SucursalService _service;
    public SucursalController(SucursalService service)
    { _service = service; }
    [HttpGet]
    public async Task<IEnumerable<SucursalDtoOut>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SucursalDtoOut>>? GetById(int id)
    {
        var sucursal = await _service.GetByIdDto(id);

        if (sucursal is null)
            return NotFound(id);

        return sucursal;
    }

    [HttpPost]
    public async Task<IActionResult> Create(SucursalDtoIn sucursal)
    {
        //creando un nuevo objeto
        var newSucursal = await _service.Create(sucursal);

        return CreatedAtAction(nameof(GetById), new { id = newSucursal.Id }, newSucursal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SucursalDtoIn sucursal)
    {
        if (id != sucursal.Id)
            return BadRequest();

        var sucursalToUpdate = await _service.GetById(id);

        if (sucursalToUpdate is not null)
        {
            await _service.Update(id, sucursal);
            return NoContent();
        }
        else
        { return NotFound(); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucursalToDelete = await _service.GetById(id);

        if (sucursalToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        { return NotFound(); }
    }
}