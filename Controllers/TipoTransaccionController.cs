using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]


    public class TipoTransaccionController : ControllerBase
    {
        private readonly ITipoTransaccionService _service;

        public TipoTransaccionController(ITipoTransaccionService tipoTransaccion)
        {
            _service =tipoTransaccion;

        }


        [HttpGet]
        public async Task<IEnumerable<TipoTransaccionDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTransaccionDtoOut>> GetById(int id)
        {
            var tipoTransaccion = await _service.GetDtoById(id);

            if (tipoTransaccion is null)
                return TipoTransaccionNotFound(id);

            return tipoTransaccion;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(TipoTransaccionDtoIn tipoTransaccion)
        {
            var newtipoTransaccion = await _service.Create(tipoTransaccion);


            return CreatedAtAction(nameof(GetById), new { id = newtipoTransaccion.Id }, newtipoTransaccion);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoTransaccionDtoIn tipoTransaccion)
        {
            if (id != tipoTransaccion.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({tipoTransaccion.Id}) del cuerpo de la solicitud." });

            var tipoTransaccionToUpdate = await _service.GetById(id);

            if (tipoTransaccionToUpdate is not null)
            {
                await _service.Update(id, tipoTransaccion);
                return Ok( new { message = $"El ID = {id} ha sido modificado!." });

            }
            else
            {
                return TipoTransaccionNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var tipoTransaccionToDelete = await _service.GetById(id);

            if (tipoTransaccionToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return TipoTransaccionNotFound(id);

            }

        }

        [NonAction]

        public NotFoundObjectResult TipoTransaccionNotFound(int id)
        {
            return NotFound(new { message = $"El tipoTransaccion con ID = {id} no existe." });
        }


    }






}