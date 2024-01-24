using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoMotivoController : ControllerBase
    {
        private readonly ITipoMotivoService _service;

        public TipoMotivoController(ITipoMotivoService tipoMotivo)
        {
            _service = tipoMotivo;

        }


        [HttpGet]
        public async Task<IEnumerable<TipoMotivoDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMotivoDtoOut>> GetById(int id)
        {
            var tipoMotivo = await _service.GetDtoById(id);

            if (tipoMotivo is null)
                return TipoMotivoNotFound(id);

            return tipoMotivo;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(TipoMotivoDtoIn tipoMotivo)
        {
            var newTipoMotivo = await _service.Create(tipoMotivo);


            return CreatedAtAction(nameof(GetById), new { id = newTipoMotivo.Id }, newTipoMotivo);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoMotivoDtoIn tipoMotivo)
        {
            if (id != tipoMotivo.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({tipoMotivo.Id}) del cuerpo de la solicitud." });

            var tipoMotivoToUpdate = await _service.GetById(id);

            if (tipoMotivoToUpdate is not null)
            {
                await _service.Update(id, tipoMotivo);
                return NoContent();

            }
            else
            {
                return TipoMotivoNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var tipoMotivoToDelete = await _service.GetById(id);

            if (tipoMotivoToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return TipoMotivoNotFound(id);

            }

        }

        [NonAction] //sirve para que no ejecute si no se lo llama

        public NotFoundObjectResult TipoMotivoNotFound(int id)
        {
            return NotFound(new { message = $"El tipoMotivo con ID = {id} no existe." });
        }
    }
}
