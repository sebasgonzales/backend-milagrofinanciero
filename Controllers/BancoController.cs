using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]



    public class BancoController : ControllerBase
    {
        private readonly IBancoService _service;

        public BancoController(IBancoService banco)
        {
            _service = banco;

        }


        [HttpGet]
        public async Task<IEnumerable<BancoDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BancoDtoOut>> GetById(int id)
        {
            var banco = await _service.GetDtoById(id);

            if (banco is null)
                return BancoNotFound(id);

            return banco;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(BancoDtoIn banco)
        {
            var newBanco = await _service.Create(banco);


            return CreatedAtAction(nameof(GetById), new { id = newBanco.Id }, newBanco);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BancoDtoIn banco)
        {
            if (id != banco.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({banco.Id}) del cuerpo de la solicitud." });

            var bancoToUpdate = await _service.GetById(id);

            if (bancoToUpdate is not null)
            {
                await _service.Update(id, banco);
                return NoContent();

            } else
            {
                return BancoNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var bancoToDelete = await _service.GetById(id);

            if (bancoToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return BancoNotFound(id);

            }

        }

        [NonAction]

        public NotFoundObjectResult BancoNotFound(int id)
        {
            return NotFound(new { message = $"El banco con ID = {id} no existe." });
        }


    }

 




}
