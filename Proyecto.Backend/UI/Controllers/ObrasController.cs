using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public ObraController(PlataformaDocenteContext context)
        {
            _context = context;
        }
        [HttpGet("por-cedula/{cedula}")]
public async Task<ActionResult<List<ObraS>>> ObtenerPorCedula(string cedula)
{
    var obras = await _context.Obras
        .Where(o => o.Cedula == cedula)
        .ToListAsync();

    // MAPEAR Obra → ObraS
    var resultado = obras.Select(o => new ObraS
    {
        IdObra = o.IdObra,
        Cedula = o.Cedula,
        Titulo = "", // <-- puedes obtenerlo si lo tienes, o dejarlo vacío
        TipoObra = o.TipoObra,
        FechaPublicacion = DateTime.Parse(o.Fecha.ToString()),
        Documento = o.Documento ?? new byte[0]
    }).ToList();

    return Ok(resultado);
}

        [HttpGet]
        public async Task<ActionResult<List<Obra>>> ObtenerTodos()
        {
            return await _context.Obras.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Obra>> ObtenerPorId(int id)
        {
            var obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.IdObra == id);

            if (obra == null)
            {
                return NotFound();
            }

            return obra;
        }

        [HttpPost]
        public async Task<ActionResult<Obra>> Crear(ObraS obraS)
        {
            var obra = new Obra
            {
                Cedula = obraS.Cedula,
                TipoObra = obraS.TipoObra,
                Fecha = obraS.FechaPublicacion, // Cambiado para usar directamente DateTime
                Documento = obraS.Documento,
            };

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = obra.IdObra }, obra);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, Obra obra)
        {
            if (id != obra.IdObra)
            {
                return BadRequest();
            }

            _context.Entry(obra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.IdObra == id);
        }

        [HttpGet("ByDocenteDesde/{cedula}/{fechaDesde}")]
        public async Task<ActionResult<List<Obra>>> GetObrasPorDocenteDesde(string cedula, string fechaDesde)
        {
            if (!DateOnly.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateOnly.FromDateTime(DateTime.Today);

            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula &&
                            DateOnly.FromDateTime(o.Fecha) > fechaDesdeParsed &&
                            DateOnly.FromDateTime(o.Fecha) <= fechaActual)
                .Select(o => new Obra
                {
                    IdObra = o.IdObra,
                    TipoObra = o.TipoObra,
                    Fecha = o.Fecha,
                    Cedula = o.Cedula
                })
                .ToListAsync();

            return obras;
        }
    }
}
