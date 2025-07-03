using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly PlataformaDocenteContext _context;
        public UsuarioController(PlataformaDocenteContext context)
        {
            _context = context;
        }
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuario()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);

        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<Usuario>> GuardarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, usuario);
        }

        [HttpPut("Actualizar/{ced}")]
        public async Task<ActionResult<Usuario>> ActualizarUsuario(string ced, Usuario usuario)
        {
            var usuarioActualizado = await _context.Usuarios.FindAsync(ced);
            if (usuarioActualizado == null)
            {
                return NotFound();
            }
            usuarioActualizado.Nom1 = usuario.Nom1;
            usuarioActualizado.Nom2 = usuario.Nom2;
            usuarioActualizado.Ape1 = usuario.Ape1;
            usuarioActualizado.Ape2 = usuario.Ape2;
            usuarioActualizado.Usuari = usuario.Usuari;
            usuarioActualizado.Pass = usuario.Pass;

            await _context.SaveChangesAsync();
            return Ok(usuarioActualizado);

        }

        [HttpDelete("Eliminar/{ced}")]
        public async Task<ActionResult<Usuario>> EliminarUsuario(string ced)
        {
            var usuario = await _context.Usuarios.FindAsync(ced);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("Buscar/{ced}")]
        public async Task<ActionResult<Usuario>> BuscarUsuario(string ced)
        {
            var usuario = await _context.Usuarios.FindAsync(ced);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        [HttpPost("Validar")]
        public async Task<ActionResult<Usuario>> ValidarUsuario([FromBody] LoginDTO login)
        {
            // 1) Validación de entrada ----------------------------
            if (login == null ||
                string.IsNullOrWhiteSpace(login.Usuari) ||
                string.IsNullOrWhiteSpace(login.Pass))
            {
                return BadRequest("Usuario y contraseña son obligatorios.");
            }

            // 2) Consulta case‑sensitive ---------------------------
            const string csCollation = "Latin1_General_CS_AS";   // CS = Case Sensitive

            var usuario = await _context.Usuarios
                .Where(u =>
                    EF.Functions.Collate(u.Usuari, csCollation) == login.Usuari &&
                    EF.Functions.Collate(u.Pass, csCollation) == login.Pass)
                .FirstOrDefaultAsync();

            // 3) Resultado ----------------------------------------
            if (usuario is null)
                return Unauthorized("Usuario o contraseña incorrectos.");
            usuario.Pass = string.Empty;        // No exponer la clave
            return Ok(usuario);
        }

        [HttpGet("porCedula/{cedula}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioPorCedula(string cedula)
        {
            var user = await _context.Usuarios
                .Where(u => u.Ced == cedula)
                .Select(u => new UsuarioDto
                {
                    Nom1 = u.Nom1,
                    Nom2 = u.Nom2,
                    Ape1 = u.Ape1,
                    Ape2 = u.Ape2,
                    Usuari = u.Usuari

                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound();

            return Ok(user);
        }


    }
}
