using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

namespace Proyecto.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public ObrasController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Obra>>> Listar()
        {
            var ob = await _context.Obras.ToListAsync();
            return Ok(ob);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Obra>> Buscar(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();
            return Ok(obra);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Obra>> Guardar(Obra obra)
        {
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = obra.IdObra }, obra);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Obra obra)
        {
            var existente = await _context.Obras.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = obra.Cedula;
            existente.TipoObra = obra.TipoObra;
            existente.Fecha = obra.Fecha;


            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Obras.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Obras.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
