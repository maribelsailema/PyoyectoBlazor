using System.Net.Http;
using System.Net.Http.Json;
using Proyecto.Shared.Models; // Cambia si tu modelo está en otro namespace

namespace Proyecto.Frontend.Services
{
    public class InvestigacionService
    {
        private readonly HttpClient _http;
        private object http;

        public InvestigacionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Investigacion>> ObtenerInvestigacionesAsync()
        {
            return await _http.GetFromJsonAsync<List<Investigacion>>("api/Investigaciones/Listar");

        }
        public async Task<List<Investigacion>> ObtenerPorCedulaAsync(string cedula)
        {
            return await _http.GetFromJsonAsync<List<Investigacion>>($"api/Investigaciones/PorCedula/{cedula}");
        }


    }
}
