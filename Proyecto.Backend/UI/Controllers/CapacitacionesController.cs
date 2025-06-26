using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapacitacionesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public CapacitacionesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Capacitacione>>> Listar()
        {
            var ap = await _context.Capacitaciones.ToListAsync();
            return Ok(ap);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Capacitacione>> Buscar(int id)
        {
            var cap = await _context.Capacitaciones.FindAsync(id);
            if (cap == null) return NotFound();
            return Ok(cap); 
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Capacitacione>> Guardar(Capacitacione cap)
        {
            _context.Capacitaciones.Add(cap);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = cap.IdCap }, cap);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Capacitacione cap)
        {
            var existente = await _context.Capacitaciones.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = cap.Cedula;
            existente.NombreCurso = cap.NombreCurso;
            existente.DuracionHoras = cap.DuracionHoras;
            existente.FechaInicio = cap.FechaInicio;


            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Capacitaciones.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Capacitaciones.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("Cedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<Capacitacione>>> ObtenerPorCedula(string cedula)
        {
            var lista = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula)
                .ToListAsync();

            if (lista == null || !lista.Any())
                return NotFound();

            return Ok(lista);

        [HttpGet("HorasTotales/{cedula}")]
        public async Task<ActionResult<int>> GetHorasTotales(string cedula)
        {
            var totalHoras = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula)
                .SumAsync(c => (int?)c.DuracionHoras) ?? 0;

            return Ok(totalHoras);
        }

    }
}
