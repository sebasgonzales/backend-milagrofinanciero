using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_milagrofinanciero.Controllers;
[ApiController]
[Route("[controller]")]
public class LocalidadController : ControllerBase
{
    private readonly LocalidadService _service;

    public LocalidadController (LocalidadService service)
    {
        _service = service;
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
        var localidadParaActualizar = await _service.GetById(id);

        if(localidadParaActualizar is not null)
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
        var localidadParaEliminar = await _service.GetById(id);
        if (localidadParaEliminar is not null)
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
