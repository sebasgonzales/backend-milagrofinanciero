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



    public class PaisController : ControllerBase
    {
        private readonly PaisService _service;

        public PaisController(PaisService pais)
        {
            _service = pais;

        }


        [HttpGet]
        public async Task<IEnumerable<PaisDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PaisDtoOut>> GetById(int id)
        {
            var pais = await _service.GetDtoById(id);

            if (pais is null)
                return PaisNotFound(id);

            return pais;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(PaisDtoIn pais)
        {
            var newPais = await _service.Create(pais);


            return CreatedAtAction(nameof(GetById), new { id = newPais.Id }, newPais);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaisDtoIn pais)
        {
            if (id != pais.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({pais.Id}) del cuerpo de la solicitud." });

            var paisToUpdate = await _service.GetById(id);

            if (paisToUpdate is not null)
            {
                await _service.Update(id, pais);
                return NoContent();

            }
            else
            {
                return PaisNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var paisToDelete = await _service.GetById(id);

            if (paisToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return PaisNotFound(id);

            }

        }

        [NonAction] //sirve para que no ejecute si no se lo llama

        public NotFoundObjectResult PaisNotFound(int id)
        {
            return NotFound(new { message = $"El pais con ID = {id} no existe." });
        }


    }
}