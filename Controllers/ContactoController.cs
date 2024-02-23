using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace backend_milagrofinanciero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _service;

        public ContactoController(IContactoService contacto)
        {
            _service = contacto;

        }


        [HttpGet]
        public async Task<IEnumerable<ContactoDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ContactoDtoOut>> GetById(int id)
        {
            var contacto = await _service.GetDtoById(id);

            if (contacto is null)
                return ContactoNotFound(id);

            return contacto;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(ContactoDtoIn contacto)
        {
            var newContacto = await _service.Create(contacto);


            return CreatedAtAction(nameof(GetById), new { id = newContacto.Id }, newContacto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContactoDtoIn contacto)
        {
            if (id != contacto.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({contacto.Id}) del cuerpo de la solicitud." });

            var contactoToUpdate = await _service.GetById(id);

            if (contactoToUpdate is not null)
            {
                await _service.Update(id, contacto);
                return NoContent();

            }
            else
            {
                return ContactoNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var contactoToDelete = await _service.GetById(id);

            if (contactoToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return ContactoNotFound(id);

            }

        }

        [NonAction]

        public NotFoundObjectResult ContactoNotFound(int id)
        {
            return NotFound(new { message = $"El contacto con ID = {id} no existe." });
        }

        [HttpGet("IdxCbu/{cbu}")]
        public async Task<ActionResult<ContactoIdDtoOut>> ObtenerIdPorCbu(string cbu, int idCuenta)
        {
            try
            {
                var contactoId = await _service.GetIdByCbu(cbu,idCuenta);

                if (contactoId is null)
                {
                    return NotFound("No se encontró un contacto con el CBU proporcionado.");
                }
                return contactoId;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
