using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public FacultadesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Facultade>>> Listar()
        {
            var fac = await _context.Facultades.ToListAsync();
            return Ok(fac);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Facultade>> Buscar(int id)
        {
            var facultad = await _context.Facultades.FindAsync(id);
            if (facultad == null) return NotFound();
            return Ok(facultad);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Facultade>> Guardar(Facultade facultad)
        {
            _context.Facultades.Add(facultad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = facultad.IdFacultad }, facultad);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Facultade facultad)
        {
            var existente = await _context.Facultades.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = facultad.Nombre;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Facultades.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Facultades.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
