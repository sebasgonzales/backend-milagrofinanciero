﻿using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]

public class EmpleadoController : ControllerBase
{

    private readonly EmpleadoService _service;

    public EmpleadoController(EmpleadoService service)
    {
        _service = service;
    }

    //GET
    [HttpGet]
    public async Task< IEnumerable<EmpleadoDtoOut>> Get() 
    {
        return await _service.GetAll();
    }

    //GET
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpleadoDtoOut>> GetById(int id)
    {
        var empleado = _service.GetById(id);

        if (empleado == null)
        {
            return NotFound();
        }
        else 
        {
            return empleado;
        }
    }     

    //POST                   
    public async Task<IActionResult> Create(EmpleadoDtoIn empleado) 
    {
        var newEmpleado = await _service.Create(empleado);

        //El siguiente metodo devuelve nuevo empleado con la funcion GetById que creamos anteriormente.
        return CreatedAtAction(nameof(GetById), new {id = empleado.Id}, empleado);
    }

    //PUT

    [HttpPut]

    public async Task<IActionResult> Update(int id, EmpleadoDtoIn empleadoDTO)
    {
        if (id != empleadoDTO.Id)
        {
            return BadRequest();
        }
        else
        {
            var empleadoToUpdate = _service.GetById(id);
            if (empleadoToUpdate is not null)
            {
                await _service.Update(id, empleadoDTO);
                return NoContent();
            }
            else
            { 
                return  NotFound();
            }
        }
    }
    // DELETE
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var empleadoToDelete = await _service.GetById(id);
        if (empleadoToDelete is not null)
        {
            await _service.Delete(id);
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
