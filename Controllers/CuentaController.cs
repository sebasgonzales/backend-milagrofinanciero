using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_milagrofinanciero.Controllers;

[ApiController]
[Route("[controller]")]
public class CuentaController : ControllerBase
{
    private readonly MilagrofinancieroG1Context _context;

    public CuentaController(MilagrofinancieroG1Context context)
    {
        _context = context;
    }
    [HttpGet]
    public IEnumerable<Cuenta> Get()
    {
        return _context.Cuenta.ToList();
    }

    [HttpGet("{id}")] 
    public ActionResult<Cuenta> GetById(int id)
    {
        var cuenta = _context.Cuenta.Find(id);

        if (cuenta == null)
        {
            return NotFound();
        }
        return cuenta;
    }

}

