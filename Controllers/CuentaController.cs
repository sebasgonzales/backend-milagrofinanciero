using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]
public class CuentaController : ControllerBase
{
    private readonly ICuentaService _service;
    public CuentaController(ICuentaService cuenta)
    {
        _service = cuenta;
    }

    [HttpGet]
    public async Task<IEnumerable<CuentaDtoOut>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")] 
    public async Task<ActionResult<CuentaDtoOut>> GetByIdDto(int id)
    {
        var cuenta = await _service.GetByIdDto(id);

        if (cuenta is null)
            return NotFound(id);
        return cuenta;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CuentaDtoIn cuenta)
    {
        var newCuenta = await _service.Create(cuenta);
        return CreatedAtAction(nameof(GetByIdDto),new {id = cuenta.Id}, newCuenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CuentaDtoIn cuenta)
    {
        if (id != cuenta.Id) return BadRequest(new { message = $"El ID ({id}) de la URL no coincide con el ID ({cuenta.Id}) del cuerpo de la solicitud." });

        var cuentaToUpdate = await _service.GetById(id);

        if (cuentaToUpdate is not null)
        {
            await _service.Update(id, cuenta);
            return NoContent();
        }
        else
        {
            return NotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task <IActionResult> Delete (int id)
    {
        var cuentaToDelete = await _service.GetById(id);

        if (cuentaToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound(id);
        }
    }

}

