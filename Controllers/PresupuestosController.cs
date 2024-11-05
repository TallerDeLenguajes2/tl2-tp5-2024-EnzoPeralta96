using Microsoft.AspNetCore.Mvc;
namespace tl2_tp5_2024_EnzoPeralta96.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{
    private  PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository("Data Source=db/Tienda.db;Cache=Shared");
    }

    [HttpGet("/api/presupuestos")]
    public ActionResult<List<Presupuestos>> Presupuestos()
    {
        return Ok(presupuestoRepository.ListarPresupuestos());
    }

    [HttpGet("/api/presupuesto/{idPrespuesto}")]
    public ActionResult<Presupuestos> ObtenerPresupuesto(int idPrespuesto)
    {
        var presupuesto =  presupuestoRepository.ObtenerPresupuesto(idPrespuesto);
        if (presupuesto is null) return NoContent();
        return presupuesto;
    }   
}
