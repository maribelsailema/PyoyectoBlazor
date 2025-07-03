using Proyecto.Shared.Models;
using System.Net.Http.Json;

namespace Proyecto.Fonted.Services
{
    public class EvaluacionDocenteService
    {
        private readonly HttpClient _http;

        public EvaluacionDocenteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EvaluacionDocente>> ObtenerPorCedulaAsync(string cedula)
        {
            var response = await _http.GetAsync($"api/EvaluacionesDocentes/porCedula/{cedula}");
            response.EnsureSuccessStatusCode();

            var evaluaciones = await response.Content.ReadFromJsonAsync<List<EvaluacionDocente>>();
            return evaluaciones ?? new List<EvaluacionDocente>();
        }

        public async Task<EvaluacionDocente?> GuardarEvaluacionAsync(EvaluacionDocente evaluacion)
        {
            var response = await _http.PostAsJsonAsync("api/EvaluacionesDocentes/Guardar", evaluacion);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<EvaluacionDocente>();
        }

        public async Task ActualizarEvaluacionAsync(EvaluacionDocente evaluacion)
        {
            var response = await _http.PutAsJsonAsync($"api/EvaluacionesDocentes/Actualizar/{evaluacion.IdEval}", evaluacion);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarEvaluacionAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/EvaluacionesDocentes/Eliminar/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
