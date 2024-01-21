using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Xml.Linq;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransaccionController: ControllerBase
    {
        private readonly ITransaccionService _service;
        public TransaccionController(ITransaccionService transaccion)
        {
            _service = transaccion;
        }
        // GET /Transaciones
        [EnableCors]

        [HttpGet]
        public async Task<IEnumerable<TransaccionDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        // GET /Transaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransaccionDtoOut>> GetById(int id)
        {
            var transaccion = await _service.GetDtoById(id);
        
            if (transaccion is null)
                    return TransaccionNotFound(id);

            return transaccion;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(TransaccionDtoIn transaccion) 
        //{
        //    var newTransaccion = await _service.Create(transaccion);

        //    return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
        //}

        [HttpPost]
        public async Task<IActionResult> Crear(TransaccionDtoIn transaccion, long numeroCuenta, float monto)
        {
            var saldoDisponible = await _service.GetSaldo(cbuOrigen, monto);

            if (saldoDisponible == 1)
            {
                var newTransaccion = await _service.Create(transaccion);

                return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
            }
            else
            {
                return Error();
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, TransaccionDtoIn transaccion)
        //{

        //    if (id != transaccion.Id)
        //        return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({transaccion.Id}) del cuerpo de la solicitud.  " });

        //    var transaccionToUpdate = await _service.GetById(id);

        //    if (transaccionToUpdate is not null)
        //    {
        //        await _service.Update(transaccion);
        //        return NoContent();
        //    }
        //    else 
        //    {
        //        return TransaccionNotFound(id);
        //    }

        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var transaccionToDelete= await _service.GetById(id);

        //    if (transaccionToDelete is not null)
        //    {
        //        await _service.Delete(id);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return TransaccionNotFound(id);
        //    }
        //}


        [HttpGet("HistorialTransacciones/{numeroCuenta}")]
        public async Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta)

        {
            var transaccionesBuscar = await _service.GetTransacciones(numeroCuenta);

            if (transaccionesBuscar is not null)
            {
                // Ordenar las transacciones desde la fecha más reciente hasta la más vieja
                var transaccionesOrdenadas = transaccionesBuscar.OrderByDescending(t => t.Realizacion);

                return transaccionesOrdenadas;
            }
            else
            {
                return Enumerable.Empty<TransaccionDtoOut>();
            }

        }

        // VER EL SALDO dee la cuenta
        [HttpGet("saldo/{numeroCuenta}")]
            public async Task<IActionResult> ObtenerSaldo(long numeroCuenta)
            {
            try
            {
                // Llamar al servicio para obtener el saldo
                var saldo = await _service.ObtenerSaldo(numeroCuenta);

                if (saldo >= 0) // Verifica si el saldo es no negativo, lo que indica que la cuenta existe
                {
                    return Ok(new { SaldoTotal = saldo });
                }
                else
                {
                    // Devolver 404 Not Found si la cuenta no existe
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un resultado apropiado
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

       


        [NonAction]
        public NotFoundObjectResult TransaccionNotFound(int id)
        {
            return NotFound(new { message = $"La transaccion con ID = {id} no existe. " });
        }

        [NonAction]
        public NotFoundObjectResult Error()
        {
            return NotFound(new { message = "No tiene saldo suficiente" });
        }
    }
}
