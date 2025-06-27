using Microsoft.AspNetCore.Mvc;
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

        // 1. Evaluaciones por docente y periodo
        [HttpGet("Evaluaciones/{cedula}/{periodo}")]
        public async Task<IActionResult> GetEvaluacionesDocente(string cedula, string periodo)
        {
            var data = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula && e.Periodo == periodo)
                .ToListAsync();
            return Ok(data);
        }

        // 2. Capacitaciones por docente
        [HttpGet("Capacitaciones/{cedula}")]
        public async Task<IActionResult> GetCapacitacionesDocente(string cedula)
        {
            var data = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula)
                .ToListAsync();
            return Ok(data);
        }

        // 3. Obras por docente
        [HttpGet("Obras/{cedula}")]
        public async Task<IActionResult> GetObrasDocente(string cedula)
        {
            var data = await _context.Obras
                .Where(o => o.Cedula == cedula)
                .ToListAsync();
            return Ok(data);
        }

        // 4. Investigaciones por docente
        [HttpGet("Investigaciones/{cedula}")]
        public async Task<IActionResult> GetInvestigacionesDocente(string cedula)
        {
            var data = await _context.Investigaciones
                .Where(i => i.Cedula == cedula)
                .ToListAsync();
            return Ok(data);
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
