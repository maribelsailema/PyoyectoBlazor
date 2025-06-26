using System.Net.Http.Json;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Frontend.Services
{
    public class ObrasService
    {
        private readonly HttpClient _http;

        public ObrasService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Obra>> ObtenerPorCedulaAsync(string cedula)
        {
            var resultado = await _http.GetFromJsonAsync<List<Obra>>($"api/capacitaciones/cedula/{cedula}");
            return resultado ?? new List<Obra>();
        }


        

    }
}