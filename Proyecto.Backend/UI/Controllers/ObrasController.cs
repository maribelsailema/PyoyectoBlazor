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

        [HttpGet("por-cedula/{cedula}")]
        public async Task<ActionResult<List<ObraS>>> ObtenerPorCedula(string cedula)
        {
            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula)
                .Select(o => new ObraS
                {
                    IdObra = o.IdObra,
                    Cedula = o.Cedula,
                    Titulo = o.Titulo,
                    TipoObra = o.TipoObra,
                    Fecha = o.Fecha,
                    Pais = o.Pais,
                    Ciudad = o.Ciudad,
                    Editorial = o.Editorial,
                    ISBN = o.ISBN,
                    DOI = o.DOI,
                    Enlace = o.Enlace,
                    Autores = o.Autores,
                    Resumen = o.Resumen,
                    Documento = o.Documento
                })
                .ToListAsync();

            return Ok(obras);
        }

        [HttpPost]
        public async Task<ActionResult<Obra>> Crear(ObraS obraS)
        {
            var obra = new Obra
            {
                Cedula = obraS.Cedula,
                Titulo = obraS.Titulo,
                TipoObra = obraS.TipoObra,
                Fecha = obraS.Fecha,
                Pais = obraS.Pais,
                Ciudad = obraS.Ciudad,
                Editorial = obraS.Editorial,
                ISBN = obraS.ISBN,
                DOI = obraS.DOI,
                Enlace = obraS.Enlace,
                Autores = obraS.Autores,
                Resumen = obraS.Resumen,
                Documento = obraS.Documento
            };

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = obra.IdObra }, obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ObraS obraS)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            obra.Titulo = obraS.Titulo;
            obra.TipoObra = obraS.TipoObra;
            obra.Fecha = obraS.Fecha;
            obra.Pais = obraS.Pais;
            obra.Ciudad = obraS.Ciudad;
            obra.Editorial = obraS.Editorial;
            obra.ISBN = obraS.ISBN;
            obra.DOI = obraS.DOI;
            obra.Enlace = obraS.Enlace;
            obra.Autores = obraS.Autores;
            obra.Resumen = obraS.Resumen;

            if (obraS.Documento != null && obraS.Documento.Length > 0)
            {
                obra.Documento = obraS.Documento;
            }

            await _context.SaveChangesAsync();

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
                            o.Fecha > fechaDesdeParsed &&
                            o.Fecha <= fechaActual)
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
