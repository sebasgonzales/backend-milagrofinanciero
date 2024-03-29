using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IEnumerable<CuentaDtoOut>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<CuentaDtoOut>> GetByIdDto(int id)
    {
        var cuenta = await _service.GetByIdDto(id);

        if (cuenta is null)
            return NotFound(id);
        return cuenta;
    }

    //cuenta interna
    [HttpPost("CuentaInterna")]
    public async Task<IActionResult> CreateCuentaInterna(CuentaDtoIn cuenta)
    {
        var newCuenta = await _service.CreateCuentaInterna(cuenta);
        return CreatedAtAction(nameof(GetByIdDto),new {id = cuenta.Id}, newCuenta);
    }

    //cuenta externa
    [HttpPost("CuentaExterna")]
    public async Task<ActionResult<Cuenta>> CreateCuentaExterna(string cbuCuentaOrigen)
    {
        var newCuenta = await _service.CreateCuentaExterna(cbuCuentaOrigen);
        return newCuenta;
    }


    [HttpPut("{id}")]
    [Authorize]
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
    [Authorize]
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

    //obtener ID x CBU
    [HttpGet("IdxCbu/{cbu}")]
    public async Task<ActionResult<CuentaIdDtoOut>> ObtenerIdPorCbu(string cbu)
    {
        try
        {
            var cuentaId = await _service.GetIdByCbu(cbu);

            if (cuentaId is null)
            {
                return NotFound("No se encontró una cuenta con el CBU proporcionado.");
            }
            return cuentaId;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    //obtener ID x NumeroCuenta
    [HttpGet("IdxNumeroCuenta/{numeroCuenta}")]
//no autorizo se usa en el registrar
    public async Task<ActionResult<CuentaIdDtoOut>> ObtenerIdPorNumeroCuenta(long numeroCuenta)
    {
        try
        {
            var cuentaId = await _service.GetIdByNumeroCuenta(numeroCuenta);

            if (cuentaId is null)
            {
                return NotFound("No se encontró una cuenta con el numeroCuenta proporcionado.");
            }
            return cuentaId;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    //cuentaId es un parametro dinamico en la ruta
    [HttpGet("cuentas/Numero/{numeroCuenta}/Contacto")] // Modificación en la ruta del endpoint
    [Authorize]
    public async Task<ActionResult<ContactoDtoOut>> GetContactos(long numeroCuenta)
    {
        var contactos = await _service.GetContactos(numeroCuenta);

        if (contactos == null)
        {
            return NotFound();
        }

        return Ok(contactos);
    }

    //obtener Cbu x NumeroCuenta
    [HttpGet("CbuxNumeroCuenta/{numeroCuenta}")]
    [Authorize]
    public async Task<ActionResult<string?>> ObtenerCbuPorNumeroCuenta(long numeroCuenta)
    {
        try
        {
            var cuentaCbu = await _service.GetCbuByNumeroCuenta(numeroCuenta);

            if (cuentaCbu is null)
            {
                return NotFound("No se encontró una cuenta con el numeroCuenta proporcionado.");
            }
            return cuentaCbu;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("CbuxId/{id}")]
    public async Task<ActionResult<string?>> ObtenerCbuPorId(int id)
    {
        try
        {
            var cuentaCbu = await _service.GetCbuById(id);

            if (cuentaCbu is null)
            {
                return NotFound("No se encontró una cuenta con el numeroCuenta proporcionado.");
            }
            return cuentaCbu;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}

