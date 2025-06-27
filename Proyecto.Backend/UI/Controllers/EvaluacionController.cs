using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Proyecto.Backend.Domain.Entities.Models; // Ajusta este using según tu estructura de carpetas

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

            var rutaArchivosSubidos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ArchivosSubidos");
            if (!Directory.Exists(rutaArchivosSubidos))
                Directory.CreateDirectory(rutaArchivosSubidos);

            var rutaCompletaArchivo = Path.Combine(rutaArchivosSubidos, archivo.FileName);
            using (var stream = new FileStream(rutaCompletaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            return Ok(new { mensaje = "Archivo recibido y guardado en el servidor.", filePath = $"/ArchivosSubidos/{archivo.FileName}" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionesDocente>>> GetEvaluaciones()
        {
            var dummyEvaluaciones = new List<EvaluacionesDocente>
            {
                new EvaluacionesDocente { IdEval = 1, Cedula = "1710000001", Periodo = "2024-1", PuntajeFinal = 95.5M, FechaEvaluacion = new DateOnly(2024, 7, 15), PdfPath = "/ArchivosSubidos/eval_juan.pdf" },
                new EvaluacionesDocente { IdEval = 2, Cedula = "1710000002", Periodo = "2024-1", PuntajeFinal = 88.0M, FechaEvaluacion = new DateOnly(2024, 7, 20), PdfPath = "/ArchivosSubidos/eval_maria.pdf" },
                new EvaluacionesDocente { IdEval = 3, Cedula = "1710000003", Periodo = "2024-2", PuntajeFinal = 75.2M, FechaEvaluacion = new DateOnly(2024, 12, 10), PdfPath = null }
            };

            await Task.Delay(500);
            return Ok(dummyEvaluaciones);
        }

        [HttpPost("agregar")]
        public IActionResult AddEvaluacion([FromBody] EvaluacionesDocente nuevaEvaluacion)
        {
            Console.WriteLine($"Recibida nueva evaluación: {nuevaEvaluacion.Cedula} - {nuevaEvaluacion.Periodo}");
            return Ok(new { message = "Evaluación agregada con éxito (simulado)." });
        }

        [HttpPut("{id}")]
        public IActionResult EditEvaluacion(int id, [FromBody] EvaluacionesDocente evaluacionActualizada)
        {
            Console.WriteLine($"Actualizando evaluación {id}: {evaluacionActualizada.Cedula} - {evaluacionActualizada.Periodo}");
            return Ok(new { message = $"Evaluación {id} actualizada con éxito (simulado)." });
        }
    }
}
