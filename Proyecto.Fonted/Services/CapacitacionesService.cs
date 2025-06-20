using System.Net.Http.Json;
using Proyecto.Shared.Models;

namespace Proyecto.Frontend.Services
{
    public class CapacitacionesService
    {
        private readonly HttpClient _http;

        public CapacitacionesService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Capacitacione>> ObtenerPorCedulaAsync(string cedula)
        {
            var resultado = await _http.GetFromJsonAsync<List<Capacitacione>>($"api/capacitaciones/cedula/{cedula}");
            return resultado ?? new List<Capacitacione>();
        }


        public async Task GuardarAsync(Capacitacione cap)
        {
            await _http.PostAsJsonAsync("api/Capacitaciones/Guardar", cap);
        }
    }
}
