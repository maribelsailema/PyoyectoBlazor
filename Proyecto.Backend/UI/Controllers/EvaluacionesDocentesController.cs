using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

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
            var lista = await _context.EvaluacionesDocentes.ToListAsync();
            return Ok(lista);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<EvaluacionesDocente>> Buscar(int id)
        {
            var evaluacion = await _context.EvaluacionesDocentes.FindAsync(id);
            if (evaluacion == null) return NotFound();
            return Ok(evaluacion);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<EvaluacionesDocente>> Guardar(EvaluacionesDocente evaluacion)
        {
            _context.EvaluacionesDocentes.Add(evaluacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = evaluacion.IdEval }, evaluacion);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, EvaluacionesDocente evaluacion)
        {
            var existente = await _context.EvaluacionesDocentes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = evaluacion.Cedula;
            existente.Periodo = evaluacion.Periodo;
            existente.PuntajeFinal = evaluacion.PuntajeFinal;
            existente.FechaEvaluacion = evaluacion.FechaEvaluacion;
            existente.Pdf = evaluacion.Pdf;

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

        [HttpGet("VerPdf/{id}")]
        public async Task<IActionResult> VerPdf(int id)
        {
            var evaluacion = await _context.EvaluacionesDocentes.FindAsync(id);
            if (evaluacion == null || evaluacion.Pdf == null)
                return NotFound("PDF no encontrado");

            return File(evaluacion.Pdf, "application/pdf");
        }

        [HttpPost("GuardarConArchivo")]
        public async Task<IActionResult> GuardarConArchivo([FromForm] IFormFile archivo, [FromForm] string cedula, [FromForm] string periodo, [FromForm] decimal puntajeFinal, [FromForm] DateTime fechaEvaluacion)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo PDF inválido");

            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);

            var evaluacion = new EvaluacionesDocente
            {
                Cedula = cedula,
                Periodo = periodo,
                PuntajeFinal = puntajeFinal,
                FechaEvaluacion = DateOnly.FromDateTime(fechaEvaluacion),
                Pdf = memoryStream.ToArray()
            };

            _context.EvaluacionesDocentes.Add(evaluacion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Evaluación guardada con PDF", id = evaluacion.IdEval });
        }
    }
}
