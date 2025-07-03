using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Dtos;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulacionesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public PostulacionesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("ArchivosPorCedula/{cedula}")]
        public async Task<ActionResult<List<ArchivoPdfDto>>> ObtenerArchivosPorCedula(string cedula)
        {
            var listaArchivos = new List<ArchivoPdfDto>();

            // Capacitaciones
            var caps = await _context.Capacitaciones
                .Where(c => c.Cedula == cedula && c.Pdf != null)
                .ToListAsync();
            listaArchivos.AddRange(caps.Select(c => new ArchivoPdfDto
            {
                Nombre = $"Capacitacion_{c.IdCap}_{c.NombreCurso}.pdf",
                PdfBase64 = Convert.ToBase64String(c.Pdf!),
                Categoria = "Capacitacion"
            }));

            // Obras (si tienes esta entidad)
            var obras = await _context.Obras
                .Where(o => o.Cedula == cedula && o.Pdf != null)
                .ToListAsync();
            listaArchivos.AddRange(obras.Select(o => new ArchivoPdfDto
            {
                Nombre = $"Obra_{o.IdObra}_{o.TipoObra}.pdf",
                PdfBase64 = Convert.ToBase64String(o.Pdf!),
                Categoria = "Obra"
            }));

            // Investigaciones (si tienes esta entidad)
            var invs = await _context.Investigaciones
                .Where(i => i.Cedula == cedula && i.Pdf != null)
                .ToListAsync();
            listaArchivos.AddRange(invs.Select(i => new ArchivoPdfDto
            {
                Nombre = $"Investigacion_{i.IdInv}_{i.NombreProyecto}.pdf",
                PdfBase64 = Convert.ToBase64String(i.Pdf!),
                Categoria = "Investigacion"
            }));

            // Aquí puedes agregar más entidades si es necesario

            return Ok(listaArchivos);
        }
    }
}
