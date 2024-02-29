using Core.DTO.request;
using Core.DTO.response;
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
    public async Task<IActionResult> CreateCuentaExterna(CuentaDtoIn cuenta)
    {
        var newCuenta = await _service.CreateCuentaExterna(cuenta);
        return CreatedAtAction(nameof(GetByIdDto), new { id = cuenta.Id }, newCuenta);
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
    /*
     * 
    //obtener cuenta x CBU
    [HttpGet("CuentaxCbu/{cbu}")]
    public async Task<IActionResult> ObtenerCuentaPorCbu(long cbu)
    {
        try
        {
            var cuenta = await _service.GetCuentaByCbu(cbu);

            if (cuenta != null)
            {
                //// Mapea la cuenta a un DTO u objeto de respuesta según sea necesario
                //var cuentaDto = new CuentaDto
                //{
                //    // Mapea las propiedades de cuenta según sea necesario
                //  Id = cuenta.Id,
                //    Numero = cuenta.Numero,
                //    Saldo = cuenta.Saldo,
                //    // ...
                //};

                return Ok(cuenta);
            }
            else
            {
                return NotFound("No se encontró ninguna cuenta con el CBU proporcionado.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
   

    //obtener cuenta x NumerodeCuenta
    [HttpGet("cuenta/{numeroCuenta}")]
    public async Task<IActionResult> ObtenerCuentaPorNumero(long numeroCuenta)
    {
        try
        {
            var cuenta = await _service.GetCuentaByNumero(numeroCuenta);

            if (cuenta != null)
            {
                //// Mapea la cuenta a un DTO u objeto de respuesta según sea necesario
                //var cuentaDto = new CuentaDto
                //{
                //    // Mapea las propiedades de cuenta según sea necesario
                //    Id = cuenta.Id,
                //    Numero = cuenta.Numero,
                //    Saldo = cuenta.Saldo,
                //    // ...
                //};

                return Ok(cuenta);
            }
            else
            {
                return NotFound("No se encontró ninguna cuenta con el NumeroDeCuente proporcionado.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    */

    //obtener ID x CBU
    [HttpGet("IdxCbu/{cbu}")]
    [Authorize]
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

}

