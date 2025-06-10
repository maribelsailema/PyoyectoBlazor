using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

namespace Proyecto.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public CarreraController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Carrera>>> Listar()
        {
            var car = await _context.Carreras.Include(c => c.IdFacultad).ToListAsync();
            return Ok(car);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Carrera>> Buscar(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null) return NotFound();
            return Ok(carrera);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Carrera>> Guardar(Carrera carrera)
        {
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = carrera.IdCarrera }, carrera);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Carrera carrera)
        {
            var existente = await _context.Carreras.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = carrera.Nombre;
            existente.IdFacultad = carrera.IdFacultad;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Carreras.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Carreras.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
