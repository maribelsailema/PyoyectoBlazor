/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor para inyectar el contexto
        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Capacitaciones/{cedula}")]
        public async Task<IActionResult> GetCapacitacionesDocente(string cedula)
        {
            var data = await _context.Set<Capacitacione>()
                .Include(c => c.CedulaNavigation)
                .Where(c => c.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(c => new ReportesResultado
            {
                Docente = $"{c.CedulaNavigation.Nom1} {c.CedulaNavigation.Ape1}",
                Tipo = "Capacitación",
                Detalle = c.NombreCurso,
                Fecha = c.FechaInicio.ToDateTime(TimeOnly.MinValue),
                ValorPuntaje = $"{c.DuracionHoras} horas"
            }).ToList();

            return Ok(resultados);
        }

        [HttpGet("Obras/{cedula}")]
        public async Task<IActionResult> GetObrasDocente(string cedula)
        {
            var data = await _context.Set<Obra>()
                .Include(o => o.CedulaNavigation)
                .Where(o => o.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(o => new ReportesResultado
            {
                Docente = $"{o.CedulaNavigation.Nom1} {o.CedulaNavigation.Ape1}",
                Tipo = "Obra",
                Detalle = o.TipoObra,
                Fecha = o.Fecha.ToDateTime(TimeOnly.MinValue),
                ValorPuntaje = "-"
            }).ToList();

            return Ok(resultados);
        }

        [HttpGet("Investigaciones/{cedula}")]
        public async Task<IActionResult> GetInvestigacionesDocente(string cedula)
        {
            var data = await _context.Set<Investigacione>()
                .Include(i => i.CedulaNavigation)
                .Where(i => i.Cedula == cedula)
                .ToListAsync();

            var resultados = data.Select(i => new ReportesResultado
            {
                Docente = $"{i.CedulaNavigation?.Nom1} {i.CedulaNavigation?.Ape1}",
                Tipo = "Investigación",
                Detalle = i.NombreProyecto,
                Fecha = i.FechaInicio.ToDateTime(TimeOnly.MinValue),
                ValorPuntaje = $"{i.TiempoMeses} meses"
            }).ToList();

            return Ok(resultados);
        }
    }
}
*/
