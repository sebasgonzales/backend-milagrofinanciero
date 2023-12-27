using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]

public class EmpleadoController : ControllerBase
{

    private readonly IEmpleadoService _service;

    public EmpleadoController(IEmpleadoService empleado)
    {
        _service = empleado;
    }

    //GET
    [HttpGet]
    public async Task< IEnumerable<EmpleadoDtoOut>> Get() 
    {
        return await _service.GetAll();
    }

    //GET By ID
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpleadoDtoOut>> GetById(int id)
    {
        var empleado = await _service.GetDtoById(id);

        if (empleado is null)
        {
            return NotFound();
        }
        else 
        {
            return empleado;
        }
    }

    //POST
    [HttpPost]
    public async Task<IActionResult> Create(EmpleadoDtoIn empleado) 
    {
        var newEmpleado = await _service.Create(empleado);

        //El siguiente metodo devuelve nuevo empleado con la funcion GetById que creamos anteriormente.
        return CreatedAtAction(nameof(GetById), new {id = empleado.Id}, newEmpleado);
    }

    //PUT

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, EmpleadoDtoIn empleado)
    {
        var empleadoToUpdate = await _service.GetById(id);
        if (empleadoToUpdate is not null)
        {
            await _service.Update(id, empleado);
            return NoContent();
        }
        else
        {
            return NotFound(id);
        }
    }
    // DELETE ID
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var empleadoToDelete = await _service.GetById(id);
        if (empleadoToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

