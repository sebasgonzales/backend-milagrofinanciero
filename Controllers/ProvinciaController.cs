using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _service;
        public ProvinciaController(IProvinciaService provincia)
        {
            _service = provincia;
        }

        [HttpGet]
        public async Task<IEnumerable<ProvinciaDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinciaDtoOut>> GetById(int id)
        {
            var provincia = await _service.GetDtoById(id);

            if (provincia is null)
                return ProvinciaNotFound(id);

            return provincia;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProvinciaDtoIn provincia)
        {

            var newProvincia = await _service.Create(provincia);

            return CreatedAtAction(nameof(GetById), new { id = newProvincia.Id }, newProvincia);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, ProvinciaDtoIn provincia)
        {

            if (id != provincia.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({provincia.Id}) del cuerpo de la solicitud.  " });

            var provinciaToUpdate = await _service.GetById(id);

            if (provinciaToUpdate is not null)
            {
                await _service.Update(id, provincia);
                return NoContent();
            }
            else
            {
                return ProvinciaNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var provinciaToDelete = await _service.GetById(id);

            if (provinciaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return ProvinciaNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult ProvinciaNotFound(int id)
        {
            return NotFound(new { message = $"la Provincia con ID = {id} no existe. " });
        }

    }
}
