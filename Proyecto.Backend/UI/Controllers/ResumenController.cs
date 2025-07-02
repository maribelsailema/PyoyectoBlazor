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
            // Obras (sin incluir PDF)
            var rolActual = await _context.RolesDocentes
    .Where(r => r.Cedula == cedula && r.Activo)
    .OrderByDescending(r => r.FechaAsignacion)
    .FirstOrDefaultAsync();

            if (rolActual == null)
                return NotFound("El docente no tiene un rol activo.");

            var fechaDesde = rolActual.FechaAsignacion;
            var fechaHasta = DateOnly.FromDateTime(DateTime.Today);


            var obras = await _context.Obras
     .Where(o => o.Cedula == cedula && o.Fecha > fechaDesde && o.Fecha <= fechaHasta)
     .Select(o => new ObraResumenDto
     {
         IdObra = o.IdObra,
         Cedula = o.Cedula,
         TipoObra = o.TipoObra,
         Fecha = o.Fecha
     })
     .ToListAsync();


            // Evaluaciones – se obtienen todas; si deseas solo la última, podrías filtrar y tomar FirstOrDefaultAsync()
            var evaluaciones = await _context.EvaluacionesDocentes
        .Where(e => e.Cedula == cedula && e.FechaEvaluacion > fechaDesde && e.FechaEvaluacion <= fechaHasta)
        .Select(e => new EvaluacionResumenDto
        {
            IdEval = e.IdEval,
            Cedula = e.Cedula,
            Periodo = e.Periodo,
            PuntajeFinal = e.PuntajeFinal,
            FechaEvaluacion = e.FechaEvaluacion
        })
        .ToListAsync();



            // Capacitaciones (sumar por registro, sin PDF)
            var capacitaciones = await _context.Capacitaciones
.Where(c => c.Cedula == cedula && c.FechaInicio > fechaDesde && c.FechaInicio <= fechaHasta)
.Select(c => new CapacitacionResumenDto
{
    IdCap = c.IdCap,
    Cedula = c.Cedula,
    NombreCurso = c.NombreCurso,
    DuracionHoras = c.DuracionHoras,
    FechaInicio = c.FechaInicio
})
.ToListAsync();


            // Investigaciones (sin PDF)
            var investigaciones = await _context.Investigaciones
.Where(i => i.Cedula == cedula && i.FechaInicio > fechaDesde && i.FechaInicio <= fechaHasta)
.Select(i => new InvestigacionResumenDto
{
    IdInv = i.IdInv,
    Cedula = i.Cedula,
    NombreProyecto = i.NombreProyecto,
    TiempoMeses = i.TiempoMeses,
    FechaInicio = i.FechaInicio,
    FechaFin = i.FechaFin,
    Tipo = i.Tipo,
    Estado = i.Estado,
    Cientifico = i.Cientifico
})
.ToListAsync();


            var resumenCompleto = new ResumenCompletoPostulanteDto
            {
                Obras = obras,
                Evaluaciones = evaluaciones,
                Capacitaciones = capacitaciones,
                Investigaciones = investigaciones
            };

            return Ok(resumenCompleto);
        }
        }
    }