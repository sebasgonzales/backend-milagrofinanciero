﻿using Microsoft.AspNetCore.Mvc;
using backend_milagrofinanciero.Services;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace backend_milagrofinanciero.Controllers

{
    [ApiController]
    [Route("[controller]")]



    public class BancoController : ControllerBase
    {
        private readonly BancoService _service;

        public BancoController(BancoService banco)
        {
            _service = banco;

        }


        [HttpGet]
        public async Task<IEnumerable<Banco>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Banco>>GetById(int id)
        {
            var banco = await _service.GetById(id);

            if (banco is null)
                return BancoNotFound(id);

            return banco;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(Banco banco)
        {
            var newBanco = await _service.Create(banco);


            return CreatedAtAction(nameof(GetById), new { id = newBanco.Id}, newBanco);
        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Banco banco)
        {
            if (id != banco.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({banco.Id}) del cuerpo de la solicitud." });

            var bancoToUpdate = await _service.GetById(id);

            if (bancoToUpdate is not null) 
            {
               await _service.Update(id, banco);
                return NoContent();
            
            }else
            {
                return BancoNotFound(id);

            }
           
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
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

        public NotFoundObjectResult BancoNotFound(int id)
        {
            return NotFound(new { message = $"El banco con ID = {id} no existe." });
        }


    }

 




}
