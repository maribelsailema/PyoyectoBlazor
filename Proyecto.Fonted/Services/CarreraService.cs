using System.Net.Http.Json;
using Proyecto.Shared.Models;

namespace Proyecto.Fonted.Services
{
    public class CarreraService
    {
        private readonly HttpClient _http;

        public CarreraService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Carrera>> ObtenerCarrerasAsync()
        {
            return await _http.GetFromJsonAsync<List<Carrera>>("api/Carrera/Listar");
        }
    }
}
