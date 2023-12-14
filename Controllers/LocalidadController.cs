using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers;
[ApiController]
[Route("[controller]")]
public class LocalidadController : ControllerBase
{
    private readonly ILocalidadService _service;

    public LocalidadController (LocalidadService localidad)
    {
        _service = localidad;
    }

    [HttpGet]
    public async Task<IEnumerable<LocalidadDtoOut>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocalidadDtoOut>>? GetById(int id)
    {
        var localidad = await _service.GetByIdDto(id);
        if (localidad is null)
        {
            return NotFound(id);
        }
        return localidad;
    }


    [HttpPost]
    public async Task<IActionResult> Create(LocalidadDtoIn localidad)
    {
        var newLocalidad = await _service.Create(localidad);
        return CreatedAtAction(nameof(GetById), new { id = localidad.Id }, newLocalidad);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,LocalidadDtoIn localidad)
    {
        var localidadToUpdate = await _service.GetById(id);

        if(localidadToUpdate is not null)
        {
            await _service.Update(id, localidad);
            return NoContent();
        }
        else
        {
            return NotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete (int id)
    {
        var localidadToDelete = await _service.GetById(id);
        if (localidadToDelete is not null)
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
