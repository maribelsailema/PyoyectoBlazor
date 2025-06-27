using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public ObraController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("por-cedula/{cedula}")]
        public async Task<ActionResult<List<Obra>>> ObtenerPorCedula(string cedula)
        {
            return await _context.Obras
                .Include(o => o.Carrera)
                .Where(o => o.Cedula == cedula)
                .ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Obra>>> ObtenerTodos()
        {
            return await _context.Obras.Include(o => o.Carrera).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Obra>> ObtenerPorId(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Carrera)
                .FirstOrDefaultAsync(o => o.IdObra == id);

            if (obra == null)
            {
                return NotFound();
            }

            return obra;
        }

        [HttpPost]
        public async Task<ActionResult<Obra>> Crear(Obra obra)
        {
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = obra.IdObra }, obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Obra obra)
        {
            if (id != obra.IdObra)
            {
                return BadRequest();
            }

            _context.Entry(obra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.IdObra == id);
        }
    }
}