using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAdmin.Models;

namespace TaskAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ApitaskContext _context;

        public TareaController(ApitaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Tarea> lista = _context.Tareas.OrderByDescending(t => t.Id).ThenBy(t => t.FechaRegistro).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody]Tarea request)
        {
            await _context.Tareas.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok("ok");
        }

        [HttpDelete]
        [Route("Cerrar/{id:int}")]
        public async Task<IActionResult> Cerrar(int id)
        {
            Tarea tarea = _context.Tareas.Where(t => t.Id == id).FirstOrDefault();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "ok");
        }
    }
}
