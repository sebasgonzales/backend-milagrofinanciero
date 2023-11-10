using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using backend_milagrofinanciero.Data.DTOS.response;
using backend_milagrofinanciero.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]
public class CuentaController : ControllerBase
{
    private readonly CuentaService _service;
    public CuentaController(CuentaService cuenta)
    {
        _service = cuenta;
    }

    [HttpGet]
    public async Task<IEnumerable<CuentaDtoOut>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")] 
    public ActionResult<Cuenta> GetById(int id)
    {
        var cuenta = _service.GetById(id);

        if (cuenta == null)
                return NotFound();
        }
        return cuenta;
    }

}

