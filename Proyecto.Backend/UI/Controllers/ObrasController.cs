using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Backend.UI.Controllers
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

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Obra>>> Listar()
        {
            var obras = await _context.Obras.ToListAsync();
            return Ok(obras);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Obra>> Buscar(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();
            return Ok(obra);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Obra>> Guardar(ObraS obraS)
        {
            var nueva = new Obra
            {
                Cedula = obraS.Cedula,
                Titulo = obraS.Titulo,
                TipoObra = obraS.TipoObra,
                Fecha = DateOnly.FromDateTime(obraS.FechaPublicacion),
                Pais = obraS.Pais,
                Ciudad = obraS.Ciudad,
                Editorial = obraS.Editorial,
                ISBN = obraS.ISBN,
                DOI = obraS.DOI,
                Enlace = obraS.Enlace,
                Autores = obraS.Autores,
                Resumen = obraS.Resumen,
                Pdf = obraS.Documento
            };

            _context.Obras.Add(nueva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Buscar), new { id = nueva.IdObra }, nueva);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, Obra obra)
        {
            var existente = await _context.Obras.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Cedula = obra.Cedula;
            existente.Titulo = obra.Titulo;
            existente.TipoObra = obra.TipoObra;
            existente.Fecha = obra.Fecha;
            existente.Pais = obra.Pais;
            existente.Ciudad = obra.Ciudad;
            existente.Editorial = obra.Editorial;
            existente.ISBN = obra.ISBN;
            existente.DOI = obra.DOI;
            existente.Enlace = obra.Enlace;
            existente.Autores = obra.Autores;
            existente.Resumen = obra.Resumen;
            existente.Pdf = obra.Pdf;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.Obras.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Obras.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("PorCedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<ObraS>>> ObtenerPorCedula(string cedula)
        {
            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula)
                .ToListAsync();

            var resultado = obras.Select(o => new ObraS
            {
                IdObra = o.IdObra,
                Cedula = o.Cedula,
                Titulo = o.Titulo,
                TipoObra = o.TipoObra,
                FechaPublicacion = o.Fecha.ToDateTime(TimeOnly.MinValue),
                Pais = o.Pais,
                Ciudad = o.Ciudad,
                Editorial = o.Editorial,
                ISBN = o.ISBN,
                DOI = o.DOI,
                Enlace = o.Enlace,
                Autores = o.Autores,
                Resumen = o.Resumen,
                Documento = o.Pdf ?? new byte[0],
                NombreArchivo = "archivo.pdf"
            }).ToList();

            return Ok(resultado);
        }

        [HttpGet("VerPdf/{id}")]
        public async Task<IActionResult> VerPdf(int id)
        {
            var obra = await _context.Obras.FindAsync(id);

            if (obra == null || obra.Pdf == null)
            {
                return NotFound("PDF no encontrado");
            }

            return File(obra.Pdf, "application/pdf");
        }

        [HttpGet("TotalObrasDesde/{cedula}/{fechaDesde}")]
        public async Task<ActionResult<int>> GetTotalObrasDesde(string cedula, string fechaDesde)
        {
            if (!DateOnly.TryParse(fechaDesde, out var fechaDesdeParsed))
                return BadRequest("Formato de fecha inválido");

            var fechaActual = DateOnly.FromDateTime(DateTime.Today);

            var total = await _context.Obras
                .Where(o => o.Cedula == cedula && o.Fecha > fechaDesdeParsed && o.Fecha <= fechaActual)
                .CountAsync();

            return Ok(total);
        }
    }
}
