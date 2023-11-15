
using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using backend_milagrofinanciero.Data.DTOS.request;
using backend_milagrofinanciero.Data.DTOS.response;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]



    public class TipoCuentaController : ControllerBase
    {
        private readonly TipoCuentaService _service;

        public TipoCuentaController(TipoCuentaService tipocuenta)
        {
            _service = tipocuenta;

        }


        [HttpGet]
        public async Task<IEnumerable<TipoCuentaDtoOut>> Get()
        { 
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCuentaDtoOut>> GetById(int id)
        {
            var tipocuenta = await _service.GetDtoById(id);

            if (tipocuenta is null)
                return TipoCuentaNotFound(id);

            return tipocuenta;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(TipoCuentaDtoIn tipocuenta)
        {
            var newTipoCuenta = await _service.Create(tipocuenta);


            return CreatedAtAction(nameof(GetById), new { id = newTipoCuenta.Id }, newTipoCuenta);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoCuentaDtoIn tipoCuenta)
        {
            if (id != tipoCuenta.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({tipoCuenta.Id}) del cuerpo de la solicitud." });

            var tipoCuentaToUpdate = await _service.GetById(id);

            if (tipoCuentaToUpdate is not null)
            {
                await _service.Update(id, tipoCuenta);
                return NoContent();

            }
            else
            {
                return TipoCuentaNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var tipoCuentaToDelete = await _service.GetById(id);

            if (tipoCuentaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return TipoCuentaNotFound(id);

            }

        }

        [NonAction]
        public NotFoundObjectResult TipoCuentaNotFound(int id)
        {
           return NotFound(new { message = $"El tipo cuenta con ID = {id} no existe." });
        }


    }






}