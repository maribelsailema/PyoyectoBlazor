using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

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
        public async Task<ActionResult<EvaluacionesDocente>> Guardar(EvaluacionDocente eval)
        {
            if (string.IsNullOrWhiteSpace(eval.Cedula) ||
        string.IsNullOrWhiteSpace(eval.Periodo) ||
        string.IsNullOrWhiteSpace(eval.TipoEvaluacion) ||
        string.IsNullOrWhiteSpace(eval.Estado) ||
        string.IsNullOrWhiteSpace(eval.ModoEvaluacion))
            {
                return BadRequest("Todos los campos obligatorios deben estar llenos.");
            }

            try
            {
                var nueva = new EvaluacionesDocente
                {
                    Cedula = eval.Cedula,
                    Periodo = eval.Periodo,
                    PuntajeFinal = eval.PuntajeFinal,
                    FechaEvaluacion = DateOnly.FromDateTime(eval.FechaEvaluacion),
                    Pdf = eval.Pdf,
                    TipoEvaluacion = eval.TipoEvaluacion,
                    Estado = eval.Estado,
                    ModoEvaluacion = eval.ModoEvaluacion
                };

                _context.EvaluacionesDocentes.Add(nueva);
                await _context.SaveChangesAsync();

                return Ok(nueva); //  Ahora devuelve el objeto guardado en JSON
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar: {ex.Message}");
            }
        }




        [HttpPut("Actualizar/{id}")]
public async Task<IActionResult> Actualizar(int id, EvaluacionDocente eval)
{
    if (id != eval.IdEval)
    {
        return BadRequest("El ID de la URL no coincide con el ID del objeto.");
    }

    var existente = await _context.EvaluacionesDocentes.FindAsync(id);
    if (existente == null) return NotFound();

            // Actualizar campos existentes
            existente.Cedula = eval.Cedula;
            existente.Periodo = eval.Periodo;
            existente.PuntajeFinal = eval.PuntajeFinal;
            existente.FechaEvaluacion = DateOnly.FromDateTime(eval.FechaEvaluacion);

            // Nuevo: actualizar campos agregados
            existente.TipoEvaluacion = eval.TipoEvaluacion;
            existente.Estado = eval.Estado;
            existente.ModoEvaluacion = eval.ModoEvaluacion;


            // Si usas DateOnly, convertir de DateTime a DateOnly
            existente.FechaEvaluacion = DateOnly.FromDateTime(eval.FechaEvaluacion);

    // Actualizar PDF solo si viene uno nuevo (evita sobrescribir con null)
    if (eval.Pdf != null && eval.Pdf.Length > 0)
    {
        existente.Pdf = eval.Pdf;
    }

    // Guardar cambios
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

        [HttpGet("porCedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<EvaluacionesDocente>>> GetPorCedula(string cedula)
        {
            var evaluaciones = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula)
                .ToListAsync();

            return Ok(evaluaciones);
        }



    }
}
