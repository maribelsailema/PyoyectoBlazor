using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

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
    try
    {
        if (login == null || string.IsNullOrWhiteSpace(login.Usuari) || string.IsNullOrWhiteSpace(login.Pass))
        {
            return BadRequest("Usuario y contraseña son obligatorios.");
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Usuari == login.Usuari && u.Pass == login.Pass);

        if (usuario == null)
        {
            return Unauthorized("Usuario o contraseña incorrectos.");
        }

        usuario.Pass = "";
        return Ok(usuario);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ ERROR en ValidarUsuario: {ex.Message}");
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
}


    }
}
