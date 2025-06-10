using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

namespace Proyecto.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigacionesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public InvestigacionesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Investigacione>>> Listar()
        {
            var inv = await _context.Investigaciones.ToListAsync();
            return Ok(inv);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Investigacione>> Buscar(int id)
        {
            var inv = await _context.Investigaciones.FindAsync(id);
            if (inv == null) return NotFound();
            return Ok(inv);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Investigacione>> Guardar(Investigacione inv)
        {
            _context.Investigaciones.Add(inv);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = inv.IdInv }, inv);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Investigacione inv)
        {
            var existente = await _context.Investigaciones.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = inv.Cedula;
            existente.NombreProyecto = inv.NombreProyecto;
            existente.TiempoMeses = inv.TiempoMeses;
            existente.FechaInicio = inv.FechaInicio;
            existente.FechaFin = inv.FechaFin;
            existente.IdCarrera = inv.IdCarrera;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Investigaciones.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Investigaciones.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
