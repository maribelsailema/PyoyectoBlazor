using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionesDocentesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public EvaluacionesDocentesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<EvaluacionesDocente>>> Listar()
        {
            var eval = await _context.EvaluacionesDocentes.ToListAsync();
            return Ok(eval);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<EvaluacionesDocente>> Buscar(int id)
        {
            var eval = await _context.EvaluacionesDocentes.FindAsync(id);
            if (eval == null) return NotFound();
            return Ok(eval);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<EvaluacionesDocente>> Guardar(EvaluacionesDocente eval)
        {
            _context.EvaluacionesDocentes.Add(eval);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = eval.IdEval }, eval);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, EvaluacionesDocente eval)
        {
            var existente = await _context.EvaluacionesDocentes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = eval.Cedula;
            existente.Periodo = eval.Periodo;
            existente.PuntajeFinal = eval.PuntajeFinal;
            existente.FechaEvaluacion = eval.FechaEvaluacion;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.EvaluacionesDocentes.FindAsync(id);
            if (existente == null) return NotFound();

            _context.EvaluacionesDocentes.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("UltimaDesde/{cedula}/{fechaDesde}")]
        public async Task<ActionResult<EvaluacionesDocente>> GetUltimaEvaluacionDesde(string cedula, string fechaDesde)
        {
            if (!DateOnly.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateOnly.FromDateTime(DateTime.Today);

            var evaluacion = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula && e.FechaEvaluacion > fechaDesdeParsed && e.FechaEvaluacion <= fechaActual)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();

            return evaluacion;
        }


    }
}
