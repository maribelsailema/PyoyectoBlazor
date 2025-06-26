using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Backend.UI.Controllers
{
    [ApiController]
    [Route("api/evaluacion")]
    public class EvaluacionController : ControllerBase
    {
        [HttpPost("importar")]
        public async Task<IActionResult> Importar([FromForm] IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido.");

            var ruta = Path.Combine("ArchivosSubidos", archivo.FileName);

            Directory.CreateDirectory("ArchivosSubidos");

            using var stream = new FileStream(ruta, FileMode.Create);
            await archivo.CopyToAsync(stream);

            // Aquí puedes llamar a un servicio que lea el archivo (CSV, Excel, etc.)

            return Ok(new { mensaje = "Archivo recibido y guardado." });
        }
    }
}
