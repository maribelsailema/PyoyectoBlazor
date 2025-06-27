using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulacionesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public PostulacionesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        // GET: api/Postulaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostulanteDto>>> GetPostulaciones()
        {
            var postulantes = await _context.SolicitudesAscenso
                .Include(s => s.Usuario)
                .Where(s => s.Estado == "En espera")
                .Select(s => new PostulanteDto
                {
                    Cedula = s.Cedula,
                    NombreCompleto = s.Usuario.Nom1 + " " + s.Usuario.Ape1,
                    Facultad = "N/A",  // Cambia esto si tienes una relación con Facultad
                    Carrera = "N/A",   // Cambia esto si tienes una relación con Carrera
                    EstadoPostulacion = s.Estado
                })
                .ToListAsync();

            return Ok(postulantes);
        }

        // PUT: api/Postulaciones/aceptar/0101010101
        [HttpPut("aceptar/{cedula}")]
        public async Task<IActionResult> Aceptar(string cedula)
        {
            var solicitud = await _context.SolicitudesAscenso
                .Where(s => s.Cedula == cedula && s.Estado == "En espera")
                .FirstOrDefaultAsync();

            if (solicitud == null) return NotFound();

            solicitud.Estado = "Aceptado";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Postulaciones/rechazar/0101010101
        [HttpPut("rechazar/{cedula}")]
        public async Task<IActionResult> Rechazar(string cedula)
        {
            var solicitud = await _context.SolicitudesAscenso
                .Where(s => s.Cedula == cedula && s.Estado == "En espera")
                .FirstOrDefaultAsync();

            if (solicitud == null) return NotFound();

            solicitud.Estado = "Rechazado";
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class PostulanteDto
    {
        public string Cedula { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public string Facultad { get; set; } = "";
        public string Carrera { get; set; } = "";
        public string EstadoPostulacion { get; set; } = "";
    }
}
