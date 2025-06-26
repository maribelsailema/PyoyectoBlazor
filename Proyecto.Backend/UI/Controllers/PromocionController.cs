using Microsoft.AspNetCore.Mvc;
using Proyecto.Backend.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public PromocionController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("VerificarRequisitos/{cedula}/{rolSolicitado}")]
        public async Task<ActionResult<bool>> VerificarRequisitos(string cedula, string rolSolicitado)
        {
            var actualRol = await _context.RolesDocentes
                .Where(r => r.Cedula == cedula)
                .OrderByDescending(r => r.FechaAsignacion).FirstOrDefaultAsync();
            if (actualRol == null) return false;

            var requisito = await _context.RequisitosAscensos
                .FirstOrDefaultAsync(r => r.DesdeRol == actualRol.Rol && r.HaciaRol == rolSolicitado);

            if (requisito == null) return false;

            var fechaAsignacion = actualRol.FechaAsignacion;
            var aniosRol = DateTime.Now.Year - fechaAsignacion.Year;

            var obras = await _context.Obras.CountAsync(o => o.Cedula == cedula);
            var eval = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();
            var cap = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula)
                .SumAsync(c => (int?)c.DuracionHoras) ?? 0;
            var inv = await _context.Investigaciones
                .Where(i => i.Cedula == cedula)
                .SumAsync(i => (int?)i.TiempoMeses) ?? 0;

            bool cumple =
                aniosRol >= requisito.TiempoMinimoAnios &&
                obras >= requisito.ObrasMinimas &&
                (eval?.PuntajeFinal ?? 0) >= requisito.PuntajeEvaluacionMinimo &&
                cap >= requisito.HorasCapacitacionMin &&
                (!requisito.MesesInvestigacionMin.HasValue || inv >= requisito.MesesInvestigacionMin);

            return Ok(cumple);
        }

        [HttpPost("Postular")]
        public async Task<IActionResult> Postular([FromBody] PostulacionDto data)
        {
            // Aquí podrías registrar la postulación en una tabla si existe
            // por ahora solo simula éxito
            return Ok(new { mensaje = "Postulación recibida" });
        }

        public class PostulacionDto
        {
            public string Cedula { get; set; }
            public string RolActual { get; set; }
            public string RolSolicitado { get; set; }
            public DateTime FechaSolicitud { get; set; }
        }
    }

}
