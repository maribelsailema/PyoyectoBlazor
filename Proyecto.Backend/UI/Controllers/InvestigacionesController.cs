using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;


namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigacionesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public InvestigacionesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Investigacione>>> Listar()
        {
            var inv = await _context.Investigaciones.ToListAsync();
            return Ok(inv);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Investigacione>> Buscar(int id)
        {
            var inv = await _context.Investigaciones.FindAsync(id);
            if (inv == null) return NotFound();
            return Ok(inv);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Investigacione>> Guardar(Investigacione inv)
        {
            var nueva = new Investigacione
            {
                Cedula = inv.Cedula,
                NombreProyecto = inv.NombreProyecto,
                TiempoMeses = inv.TiempoMeses,
                FechaInicio = inv.FechaInicio,
                FechaFin = inv.FechaFin,
                Pdf = inv.Pdf,
                IdCarrera = inv.IdCarrera,
                Tipo = inv.Tipo,
                Estado = inv.Estado,
                Cientifico = inv.Cientifico
            };

            _context.Investigaciones.Add(nueva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Buscar), new { id = nueva.IdInv }, nueva);
        }


        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Investigacione inv)
        {
            var existente = await _context.Investigaciones.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = inv.Cedula;
            existente.NombreProyecto = inv.NombreProyecto;
            existente.TiempoMeses = inv.TiempoMeses;
            existente.FechaInicio = inv.FechaInicio;
            existente.FechaFin = inv.FechaFin;
            existente.IdCarrera = inv.IdCarrera;
            existente.Tipo = inv.Tipo;
            existente.Estado = inv.Estado;
            existente.Cientifico = inv.Cientifico;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Investigaciones.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Investigaciones.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("MesesTotalesDesde/{cedula}/{fechaDesde}")]
        public async Task<ActionResult<int>> GetMesesTotalesDesde(string cedula, string fechaDesde)
        {
            if (!DateOnly.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateOnly.FromDateTime(DateTime.Today);

            var meses = await _context.Investigaciones
                .Where(i => i.Cedula == cedula && i.FechaInicio > fechaDesdeParsed && i.FechaInicio <= fechaActual)
                .SumAsync(i => i.TiempoMeses);

            return meses;
        }



        [HttpGet("PorCedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<Proyecto.Shared.Models.Investigacion>>> ObtenerPorCedula(string cedula)
        {
            var investigaciones = await _context.Investigaciones
    .Include(i => i.IdCarreraNavigation)
    .Where(i => i.Cedula == cedula)
    .ToListAsync();

            var resultado = investigaciones.Select(i => new Proyecto.Shared.Models.Investigacion
            {
                IdInv = i.IdInv,
                Cedula = i.Cedula,
                NombreProyecto = i.NombreProyecto,
                TiempoMeses = i.TiempoMeses,
                FechaInicio = i.FechaInicio.ToDateTime(TimeOnly.MinValue),
                FechaFin = i.FechaFin?.ToDateTime(TimeOnly.MinValue),
                Pdf = i.Pdf,
                IdCarrera = i.IdCarrera,
                NombreCarrera = i.IdCarreraNavigation?.Nombre,
                Tipo = i.Tipo,          
                Estado = i.Estado,
                Cientifico= i.Cientifico
            }).ToList();

            return Ok(resultado);
        }

        [HttpGet("VerPdf/{id}")]
        public async Task<IActionResult> VerPdf(int id)
        {
            var investigacion = await _context.Investigaciones.FindAsync(id);

            if (investigacion == null || investigacion.Pdf == null)
            {
                return NotFound("PDF no encontrado");
            }

            return File(investigacion.Pdf, "application/pdf");
        }





    }



}
