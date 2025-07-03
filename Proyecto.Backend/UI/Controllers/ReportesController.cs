/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public ReportesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Evaluaciones/{cedula}/{periodo}")]
        public async Task<IActionResult> GetEvaluacionesDocente(string cedula, string periodo)
        {
            var data = await _context.EvaluacionesDocentes
                .Include(e => e.CedulaNavigation)
                .Where(e => e.Cedula == cedula && e.Periodo == periodo)
                .ToListAsync();

            var resultados = data.Select(e => new ReportesResultado
            {
                Docente = $"{e.CedulaNavigation.Nom1} {e.CedulaNavigation.Ape1}",
                Tipo = "Evaluación Docente",
                Detalle = $"Periodo: {e.Periodo}",
                Fecha = e.FechaEvaluacion.ToDateTime(new TimeOnly()),
                ValorPuntaje = $"{e.PuntajeFinal}"
            }).ToList();

            return Ok(resultados);
        }

        [HttpGet("Capacitaciones/{cedula}")]
        public async Task<IActionResult> GetCapacitacionesDocente(string cedula)
        {
            var data = await _context.Capacitaciones
                .Include(c => c.CedulaNavigation)
                .Where(c => c.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(c => new ReportesResultado
            {
                Docente = $"{c.CedulaNavigation.Nom1} {c.CedulaNavigation.Ape1}",
                Tipo = "Capacitación",
                Detalle = c.NombreCurso,
                Fecha = c.FechaInicio.ToDateTime(new TimeOnly()),
                ValorPuntaje = $"{c.DuracionHoras} horas"
            }).ToList();

            return Ok(resultados);
        }

        [HttpGet("Obras/{cedula}")]
        public async Task<IActionResult> GetObrasDocente(string cedula)
        {
            var data = await _context.Obras
                .Include(o => o.CedulaNavigation)
                .Where(o => o.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(o => new ReportesResultado
            {
                Docente = $"{o.CedulaNavigation.Nom1} {o.CedulaNavigation.Ape1}",
                Tipo = "Obra",
                Detalle = o.TipoObra,
                Fecha = o.Fecha.ToDateTime(new TimeOnly()),
                ValorPuntaje = "-"
            }).ToList();

            return Ok(resultados);
        }

        [HttpGet("Investigaciones/{cedula}")]
        public async Task<IActionResult> GetInvestigacionesDocente(string cedula)
        {
            var data = await _context.Investigaciones
                .Include(i => i.CedulaNavigation)
                .Where(i => i.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(i => new ReportesResultado
            {
                Docente = $"{i.CedulaNavigation?.Nom1} {i.CedulaNavigation?.Ape1}",
                Tipo = "Investigación",
                Detalle = i.NombreProyecto,
                Fecha = i.FechaInicio.ToDateTime(new TimeOnly()),
                ValorPuntaje = $"{i.TiempoMeses} meses"
            }).ToList();

            return Ok(resultados);
        }


        // 5. Rol actual y antigüedad
        [HttpGet("RolYAntiguedad/{cedula}")]
        public async Task<IActionResult> GetRolYAntiguedad(string cedula)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Ced == cedula);
            var rol = await _context.RolesDocentes // <== corregido aquí
                .FirstOrDefaultAsync(r => r.Cedula == cedula && r.Activo);

            if (usuario == null || rol == null)
                return NotFound();

            var antiguedad = usuario.FechaIngreso.HasValue
                ? DateTime.Now.Year - usuario.FechaIngreso.Value.Year
                : 0;

            return Ok(new
            {
                Cedula = usuario.Ced,
                Nombre = $"{usuario.Nom1} {usuario.Ape1}",
                Rol = rol.Rol,
                Antiguedad = antiguedad
            });
        }

    }
}

*/