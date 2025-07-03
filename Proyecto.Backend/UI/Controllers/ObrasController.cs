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

            // Mapear Obra → ObraS
            var resultado = obras.Select(o => new ObraS
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
                PDF = o.PDF
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
                PDF = obraS.PDF
            };

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = obra.IdObra }, obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ObraS obraS)
        {
            if (id != obraS.IdObra)
            {
                return BadRequest();
            }

            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            // Actualizar campos
            obra.Cedula = obraS.Cedula;
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
            obra.PDF = obraS.PDF;

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
        public async Task<ActionResult<List<ObraS>>> GetObrasPorDocenteDesde(string cedula, string fechaDesde)
        {
            if (!DateTime.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateTime.Today;

            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula && o.Fecha > fechaDesdeParsed && o.Fecha <= fechaActual)
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
                    PDF = o.PDF
                })
                .ToListAsync();

            return obras;
        }
    }
}