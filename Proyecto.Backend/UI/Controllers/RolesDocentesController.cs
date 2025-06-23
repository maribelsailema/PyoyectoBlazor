using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesDocentesController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;

        public RolesDocentesController(PlataformaDocenteContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<RolesDocente>>> Listar()
        {
            var rol = await _context.RolesDocentes.ToListAsync();

            return Ok(rol);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<RolesDocente>> Buscar(int id)
        {
            var rol = await _context.RolesDocentes.FindAsync(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<RolesDocente>> Guardar(RolesDocente rol)
        {
            if (rol.Activo)
            {
                // Desactiva roles anteriores del docente si se activa uno nuevo
                var existentes = await _context.RolesDocentes
                    .Where(r => r.Cedula == rol.Cedula && r.Activo)
                    .ToListAsync();

                foreach (var r in existentes)
                {
                    r.Activo = false;
                }
            }

            _context.RolesDocentes.Add(rol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = rol.IdRol }, rol);
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, RolesDocente rol)
        {
            var existente = await _context.RolesDocentes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Rol = rol.Rol;
            existente.FechaAsignacion = rol.FechaAsignacion;
            existente.Activo = rol.Activo;

            if (rol.Activo)
            {
                var otros = await _context.RolesDocentes
                    .Where(r => r.Cedula == existente.Cedula && r.IdRol != id && r.Activo)
                    .ToListAsync();

                foreach (var r in otros)
                {
                    r.Activo = false;
                }
            }

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existente = await _context.RolesDocentes.FindAsync(id);
            if (existente == null) return NotFound();

            _context.RolesDocentes.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    

    [HttpGet("RolActual/{cedula}")]
        public async Task<ActionResult<object>> ObtenerRolActual(string cedula)
        {
            var rol = await _context.RolesDocentes
                .Where(r => r.Cedula == cedula && r.Activo)
                .Select(r => new
                {
                    r.Rol,
                    r.FechaAsignacion,
                    AñosEnRol = EF.Functions.DateDiffYear(
                     r.FechaAsignacion.ToDateTime(TimeOnly.MinValue),
                      DateTime.Today)

                })
                .FirstOrDefaultAsync();

            if (rol == null) return NotFound("No se encontró un rol activo.");

            return Ok(rol);
        }

    }
}
