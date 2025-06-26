using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
// Asegúrate de que este namespace apunte a tu clase EvaluacionesDocente en Proyecto.Shared
// o donde la tengas definida en tu Backend si no usas Proyecto.Shared.
// En el ejemplo de Blazor anterior, puse la clase en el mismo .razor,
// pero la práctica recomendada es que esté en Proyecto.Shared.Models.
using Proyecto.Backend.Domain.Entities.Models; // Asumiendo que tu modelo está aquí

namespace Proyecto.Backend.UI.Controllers
{
    [ApiController]
    [Route("api/evaluacion")]
    public class EvaluacionController : ControllerBase
    {
        // Tu método POST existente para importar archivos
        [HttpPost("importar")]
        public async Task<IActionResult> Importar([FromForm] IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido.");

            // Define la ruta donde guardarás el archivo en el servidor backend.
            // Asegúrate de que esta carpeta exista y tenga permisos de escritura.
            var rutaArchivosSubidos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ArchivosSubidos");

            // Crea el directorio si no existe
            if (!Directory.Exists(rutaArchivosSubidos))
            {
                Directory.CreateDirectory(rutaArchivosSubidos);
            }

            var rutaCompletaArchivo = Path.Combine(rutaArchivosSubidos, archivo.FileName);

            using (var stream = new FileStream(rutaCompletaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Aquí normalmente procesarías el archivo (ej. leer un Excel/CSV)
            // y guardarías los datos en la base de datos,
            // y tal vez asociarías la ruta del PDF/archivo a una evaluación existente
            // o crearías una nueva entrada de EvaluaciónDocente.
            // Por simplicidad, solo confirmamos la recepción.

            return Ok(new { mensaje = "Archivo recibido y guardado en el servidor.", filePath = $"/ArchivosSubidos/{archivo.FileName}" });
        }

        // NUEVO MÉTODO GET para obtener la lista de evaluaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionesDocente>>> GetEvaluaciones()
        {
            // NOTA: En un proyecto real, aquí deberías obtener los datos de tu base de datos
            // usando Entity Framework Core (o tu ORM/acceso a datos).
            // Por ejemplo:
            // var evaluaciones = await _context.EvaluacionesDocente.ToListAsync();
            // return Ok(evaluaciones);

            // *** Para el propósito de demostración y pruebas con el frontend,
            //     devolvemos una lista de datos "dummy" (ficticios). ***
            var dummyEvaluaciones = new List<EvaluacionesDocente>
            {
                new EvaluacionesDocente
                {
                    IdEval = 1, Cedula = "1710000001", Periodo = "2024-1", PuntajeFinal = 95.5M,
                    FechaEvaluacion = new DateOnly(2024, 7, 15), PdfPath = "/ArchivosSubidos/eval_juan.pdf"
                },
                new EvaluacionesDocente
                {
                    IdEval = 2, Cedula = "1710000002", Periodo = "2024-1", PuntajeFinal = 88.0M,
                    FechaEvaluacion = new DateOnly(2024, 7, 20), PdfPath = "/ArchivosSubidos/eval_maria.pdf"
                },
                new EvaluacionesDocente
                {
                    IdEval = 3, Cedula = "1710000003", Periodo = "2024-2", PuntajeFinal = 75.2M,
                    FechaEvaluacion = new DateOnly(2024, 12, 10), PdfPath = null
                }
            };

            // Simula un pequeño retardo para mostrar el estado de carga en el frontend
            await Task.Delay(500);

            return Ok(dummyEvaluaciones);
        }

        // Placeholder para agregar una nueva evaluación (si fuera necesario por la UI)
        [HttpPost("agregar")]
        public IActionResult AddEvaluacion([FromBody] EvaluacionesDocente nuevaEvaluacion)
        {
            // Aquí se implementaría la lógica para guardar la nueva evaluación en la DB
            Console.WriteLine($"Recibida nueva evaluación: {nuevaEvaluacion.Cedula} - {nuevaEvaluacion.Periodo}");
            // nuevaEvaluacion.IdEval = ... generar ID si no es IDENTITY
            // _context.EvaluacionesDocente.Add(nuevaEvaluacion);
            // await _context.SaveChangesAsync();
            return Ok(new { message = "Evaluación agregada con éxito (simulado)." });
        }

        // Placeholder para editar una evaluación existente
        [HttpPut("{id}")]
        public IActionResult EditEvaluacion(int id, [FromBody] EvaluacionesDocente evaluacionActualizada)
        {
            // Aquí se implementaría la lógica para actualizar la evaluación en la DB
            Console.WriteLine($"Actualizando evaluación {id}: {evaluacionActualizada.Cedula} - {evaluacionActualizada.Periodo}");
            // var existingEval = await _context.EvaluacionesDocente.FindAsync(id);
            // if (existingEval == null) return NotFound();
            // existingEval.Cedula = evaluacionActualizada.Cedula;
            // ... actualizar otros campos
            // await _context.SaveChangesAsync();
            return Ok(new { message = $"Evaluación {id} actualizada con éxito (simulado)." });
        }
    }
}