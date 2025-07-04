using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public ObrasController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("por-cedula/{cedula}")]
        public async Task<ActionResult<List<ObraS>>> ObtenerPorCedula(string cedula)
        {
            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula)
                .ToListAsync();

            var resultado = obras.Select(MapObraToObraS).ToList();
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<ActionResult<List<ObraS>>> ObtenerTodos()
        {
            var obras = await _context.Obras.ToListAsync();
            return obras.Select(MapObraToObraS).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObraS>> ObtenerPorId(int id)
        {
            var obra = await _context.Obras.FirstOrDefaultAsync(o => o.IdObra == id);

            if (obra == null)
                return NotFound();

            return MapObraToObraS(obra);
        }

        [HttpPost]
        public async Task<ActionResult<ObraS>> Crear(ObraS obraS)
        {
            var obra = MapObraSToObra(obraS);

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            obraS.IdObra = obra.IdObra;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = obra.IdObra }, obraS);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ObraS obraS)
        {
            if (id != obraS.IdObra)
                return BadRequest();

            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
                return NotFound();

            // Actualizar campos
            obra.Cedula = obraS.Cedula;
            obra.Titulo = obraS.Titulo;
            obra.TipoObra = obraS.TipoObra;
            obra.Fecha = obraS.FechaPublicacion.Date;
            obra.Pais = obraS.Pais;
            obra.Ciudad = obraS.Ciudad;
            obra.Editorial = obraS.Editorial;
            obra.ISBN = obraS.ISBN;
            obra.DOI = obraS.DOI;
            obra.Enlace = obraS.Enlace;
            obra.Autores = obraS.Autores;
            obra.Resumen = obraS.Resumen;
            obra.Documento = obraS.Documento;

            _context.Entry(obra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
                return NotFound();

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.IdObra == id);
        }

        [HttpGet("ByDocenteDesde/{cedula}/{fechaDesde}")]
        public async Task<ActionResult<List<ObraS>>> GetObrasPorDocenteDesde(string cedula, string fechaDesde)
        {
            if (!DateTime.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateTime.Today;

            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula &&
                            o.Fecha > fechaDesdeParsed &&
                            o.Fecha <= fechaActual)
                .ToListAsync();

            return obras.Select(MapObraToObraS).ToList();
        }

        // Métodos de mapeo
        private static ObraS MapObraToObraS(Obra o) => new ObraS
        {
            IdObra = o.IdObra,
            Cedula = o.Cedula,
            Titulo = o.Titulo,
            TipoObra = o.TipoObra,
            FechaPublicacion = o.Fecha, // Cambiado para usar directamente DateTime
            Pais = o.Pais,
            Ciudad = o.Ciudad,
            Editorial = o.Editorial,
            ISBN = o.ISBN,
            DOI = o.DOI,
            Enlace = o.Enlace,
            Autores = o.Autores,
            Resumen = o.Resumen,
            Documento = o.Documento
        };

        private static Obra MapObraSToObra(ObraS s) => new Obra
        {
            IdObra = s.IdObra,
            Cedula = s.Cedula,
            Titulo = s.Titulo,
            TipoObra = s.TipoObra,
            Fecha = s.FechaPublicacion,
            Pais = s.Pais,
            Ciudad = s.Ciudad,
            Editorial = s.Editorial,
            ISBN = s.ISBN,
            DOI = s.DOI,
            Enlace = s.Enlace,
            Autores = s.Autores,
            Resumen = s.Resumen,
            Documento = s.Documento
        };
    }
}