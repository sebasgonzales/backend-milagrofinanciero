using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Diagnostics;
using System.Xml.Linq;

namespace backend_milagrofinanciero.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TransaccionController: ControllerBase
    {
        private readonly ITransaccionService _service;
        private readonly ICuentaService _cuentaService;
        public TransaccionController(ITransaccionService transaccion, ICuentaService cuentaService)
        {
            _service = transaccion;
            _cuentaService = cuentaService;
        }

     
        // GET /Transaciones
        [EnableCors]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TransaccionDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        // GET /Transaciones/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TransaccionDtoOut>> GetById(int id)
        {
            var transaccion = await _service.GetDtoById(id);
        
            if (transaccion is null)
                    return TransaccionNotFound(id);

            return transaccion;
        }

        [HttpPost ("TransaccionDesdeExterior")]
        public async Task<IActionResult> CreateTransaccionExterna(TransaccionExternaDtoIn transaccion) 
        {
            var newTransaccion = await _service.CreateTransaccionExterna(transaccion);

            return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
        }

        //TRANSACCION INTERNA
        [HttpPost("TransaccionInterna")]
        public async Task<IActionResult> CrearTransaccionInterna(TransaccionDtoIn transaccion, long numeroCuentaOrigen, string cbuDestino, float monto)
        {
            var saldoDisponible = 0; // 1 = Tiene saldo Disponible | 0 = No
            Debug.WriteLine("Saldo disponible: " + saldoDisponible);
            if (numeroCuentaOrigen == 111396740353)
            {
                Debug.WriteLine("La cuenta de origen es 111396740353, asignando saldo de 10000.");
                saldoDisponible = 1; 
                Debug.WriteLine(saldoDisponible);
            }
            else
            {
                //verifico el saldo si no es de la cuenta del banco
                Debug.WriteLine("Ingrese en el else. " + saldoDisponible);
                saldoDisponible = await _service.VerificadorSaldo(numeroCuentaOrigen, monto);
            }
            // Verificar que el monto en el cuerpo JSON sea igual al parámetro 'monto'
            if (transaccion.Monto != monto)
            {
                // Manejar el caso en que los montos no coinciden
                return BadRequest("El monto en el cuerpo JSON no coincide con el parámetro 'monto' en la solicitud.");
            }
            if (saldoDisponible == 1)
            {
                // Obtener el ID de la cuenta de destino a partir del CBU
                CuentaIdDtoOut cuentaDestinoId = await _cuentaService.GetIdByCbu(cbuDestino);

                // Obtener el ID de la cuenta de origen a partir del Numero
                //var cuentaOrigenId = await _cuentaService.GetIdByNumeroCuenta(numeroCuentaOrigen);
                CuentaIdDtoOut cuentaOrigenDto = await _cuentaService.GetIdByNumeroCuenta(numeroCuentaOrigen);

                if (cuentaDestinoId != null && cuentaOrigenDto != null)
                {
                    // Configurar la información de la transacción

                    transaccion.IdCuentaOrigen = cuentaOrigenDto.Id;
                    transaccion.IdCuentaDestino = cuentaDestinoId.Id;
                    //transaccion.IdTipoTransaccion = ;
                    // transaccion.IdTipoTransaccion = /* IdTipoTransaccion según sea necesario */;

                    // Crear la transacción
                    var newTransaccion = await _service.CreateTransaccionInterna(transaccion);

                    return CreatedAtAction(nameof(GetById), new { id = newTransaccion.Id }, newTransaccion);
                }
                else
                {
                    return Error();

                }
            }
            else
            {

                // Manejar el caso en que no hay saldo suficiente
                return BadRequest("Saldo insuficiente para realizar la transferencia.");
            }
        }

        [HttpGet("HistorialTransacciones/{numeroCuenta}")]
        public async Task<IEnumerable<TransaccionDtoOut>> GetTransacciones(long numeroCuenta)

        {
            var transaccionesBuscar = await _service.GetTransacciones(numeroCuenta);

            if (transaccionesBuscar is not null)

            {
                // Ordenar las transacciones desde la fecha más reciente hasta la más vieja
                var transaccionesOrdenadas = transaccionesBuscar.OrderByDescending(t => t.Numero);


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
                if (saldo < 0)
                {
                    return BadRequest(new { saldoNegativo = saldo });
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
