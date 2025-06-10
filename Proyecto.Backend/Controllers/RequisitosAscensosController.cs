using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

namespace Proyecto.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitosAscensosController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public RequisitosAscensosController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<RequisitosAscenso>>> Listar()
        {
            var req = await _context.RequisitosAscensos.ToListAsync();
            return Ok(req);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<RequisitosAscenso>> Buscar(int id)
        {
            var requisito = await _context.RequisitosAscensos.FindAsync(id);
            if (requisito == null) return NotFound();
            return Ok(requisito);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<RequisitosAscenso>> Guardar(RequisitosAscenso requisito)
        {
            _context.RequisitosAscensos.Add(requisito);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = requisito.Id }, requisito);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, RequisitosAscenso requisito)
        {
            var existente = await _context.RequisitosAscensos.FindAsync(id);
            if (existente == null) return NotFound();

            existente.DesdeRol = requisito.DesdeRol;
            existente.HaciaRol = requisito.HaciaRol;
            existente.TiempoMinimoAnios = requisito.TiempoMinimoAnios;
            existente.ObrasMinimas = requisito.ObrasMinimas;
            existente.PuntajeEvaluacionMinimo = requisito.PuntajeEvaluacionMinimo;
            existente.HorasCapacitacionMin = requisito.HorasCapacitacionMin;
            existente.MesesInvestigacionMin = requisito.MesesInvestigacionMin;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.RequisitosAscensos.FindAsync(id);
            if (existente == null) return NotFound();

            _context.RequisitosAscensos.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
