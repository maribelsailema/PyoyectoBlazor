using Microsoft.AspNetCore.Mvc;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;
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

        [HttpGet("Todas")]
        public async Task<ActionResult<IEnumerable<PostulacionDto>>> GetTodas()
        {
            var postulaciones = await _context.Postulaciones
        .Where(p => p.Estado == "Pendiente" || p.Estado == "Aceptada")
        // <- filtro agregado
        .Select(p => new PostulacionDto
        {
            Id = p.Id,
            Cedula = p.Cedula,
            RolActual = p.RolActual,
            RolSolicitado = p.RolSolicitado,
            FechaSolicitud = p.FechaSolicitud,
            Estado = p.Estado
        })
        .ToListAsync();

            return Ok(postulaciones);
        }

        [HttpPut("CambiarEstado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null) return NotFound();

            postulacion.Estado = nuevoEstado;

            if (nuevoEstado == "Aceptada")
            {
                // Desactivar roles anteriores
                var rolesAnteriores = await _context.RolesDocentes
                    .Where(r => r.Cedula == postulacion.Cedula && r.Activo)
                    .ToListAsync();

                foreach (var rol in rolesAnteriores)
                    rol.Activo = false;

                // Agregar nuevo rol
                var nuevoRol = new RolesDocente
                {
                    Cedula = postulacion.Cedula,
                    Rol = postulacion.RolSolicitado,
                    FechaAsignacion = DateOnly.FromDateTime(DateTime.Today),
                    Activo = true
                };
                _context.RolesDocentes.Add(nuevoRol);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }



        [HttpPost("Postular")]
        public async Task<IActionResult> Postular([FromBody] PostulacionDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Cedula) || string.IsNullOrWhiteSpace(dto.RolActual) || string.IsNullOrWhiteSpace(dto.RolSolicitado))
            {
                return BadRequest("Datos inválidos.");
            }

            var nueva = new Postulacion
            {
                Cedula = dto.Cedula,
                RolActual = dto.RolActual,
                RolSolicitado = dto.RolSolicitado,
                FechaSolicitud = dto.FechaSolicitud != DateTime.MinValue ? dto.FechaSolicitud : DateTime.Now,
                Estado = "Pendiente"
            };

            _context.Postulaciones.Add(nueva);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("PostulacionPorCedula/{cedula}")]
        public async Task<ActionResult<PostulacionDto>> GetPostulacionPorCedula(string cedula)
        {
            var postulacion = await _context.Postulaciones
                .Where(p => p.Cedula == cedula && p.Estado == "Pendiente")  // O puedes quitar estado para obtener la última o todas
                .OrderByDescending(p => p.FechaSolicitud)
                .FirstOrDefaultAsync();

            if (postulacion == null)
                return NotFound();

            var dto = new PostulacionDto
            {
                Id = postulacion.Id,
                Cedula = postulacion.Cedula,
                RolActual = postulacion.RolActual,
                RolSolicitado = postulacion.RolSolicitado,
                FechaSolicitud = postulacion.FechaSolicitud,
                Estado = postulacion.Estado
            };

            return Ok(dto);
        }

        [HttpGet("ResumenDocumentos/{cedula}")]
        public async Task<ActionResult<ResumenDocumentosDto>> GetResumenDocumentos(string cedula)
        {
            var fechaDesde = await _context.RolesDocentes
                .Where(r => r.Cedula == cedula && r.Activo)
                .Select(r => r.FechaAsignacion)
                .FirstOrDefaultAsync();

            if (fechaDesde == default) return NotFound("Rol no encontrado");

            var totalObras = await _context.Obras
                .Where(o => o.Cedula == cedula && o.Fecha >= fechaDesde)
                .CountAsync();

            var evaluacion = await _context.EvaluacionesDocentes
                .Where(e => e.Cedula == cedula && e.FechaEvaluacion >= fechaDesde)
                .OrderByDescending(e => e.FechaEvaluacion)
                .FirstOrDefaultAsync();

            var totalHorasCap = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula && c.FechaInicio >= fechaDesde)
                .SumAsync(c => (int?)c.DuracionHoras) ?? 0;

            var totalMesesInv = await _context.Investigaciones
                .Where(i => i.Cedula == cedula && i.FechaInicio >= fechaDesde)
                .SumAsync(i => EF.Functions.DateDiffMonth(i.FechaInicio, i.FechaFin ?? DateOnly.FromDateTime(DateTime.Today)));

            var resumen = new ResumenDocumentosDto
            {
                TotalObras = totalObras,
                PuntajeEvaluacion = evaluacion?.PuntajeFinal,
                TotalHorasCapacitacion = totalHorasCap,
                TotalMesesInvestigacion = totalMesesInv
            };

            return Ok(resumen);
        }

    }
}


