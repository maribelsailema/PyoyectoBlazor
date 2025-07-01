using Microsoft.AspNetCore.Http;
// Al inicio del archivo .cs
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public ResumenController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Postulante/{cedula}")]
        public async Task<ActionResult<ResumenPostulanteDto>> GetResumenPostulante(string cedula)
        {
            var rolActual = await _context.RolesDocentes
                .Where(r => r.Cedula == cedula && r.Activo)
                .OrderByDescending(r => r.FechaAsignacion)
                .FirstOrDefaultAsync();

            if (rolActual == null)
                return NotFound("No se encontró un rol activo para el docente.");

            var fechaDesde = rolActual.FechaAsignacion;
            var fechaActual = DateOnly.FromDateTime(DateTime.Today);

            // Obras
            var cantidadObras = await _context.Obras
                .Where(o => o.Cedula == cedula && o.Fecha > fechaDesde && o.Fecha <= fechaActual)
                .CountAsync();

            // Evaluación más reciente en el rango
            var evaluacion = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula && e.FechaEvaluacion > fechaDesde && e.FechaEvaluacion <= fechaActual)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();

            // Capacitaciones
            var horasCap = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula && c.FechaInicio > fechaDesde && c.FechaInicio <= fechaActual)
                .SumAsync(c => c.DuracionHoras);

            // Investigaciones
            var mesesInv = await _context.Investigaciones
                .Where(i => i.Cedula == cedula && i.FechaInicio > fechaDesde && i.FechaInicio <= fechaActual)
                .SumAsync(i => i.TiempoMeses);

            // Construcción del resumen
            var resumen = new ResumenPostulanteDto
            {
                CantidadObras = cantidadObras,
                PuntajeEvaluacion = evaluacion?.PuntajeFinal,
                DuracionHoras = horasCap,
                TiempoMeses = mesesInv
            };

            return Ok(resumen);
        }
    }
}
