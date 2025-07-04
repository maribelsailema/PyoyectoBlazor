using System.Net.Http.Json;
using Proyecto.Shared.Models;

namespace Proyecto.Frontend.Services
{
    public class ObraService
    {
        private readonly HttpClient _httpClient;

        public ObraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ObraS>> ObtenerPorCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/Obras/por-cedula/{cedula}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ObraS>>() ?? new List<ObraS>();
            }
            return new List<ObraS>();
        }

        public async Task<ObraS?> GuardarObraAsync(ObraS obra)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Obra", obra);
            return await response.Content.ReadFromJsonAsync<ObraS>();
        }

        public async Task ActualizarObraAsync(ObraS obra)
        {
            await _httpClient.PutAsJsonAsync($"api/Obra/{obra.IdObra}", obra);
        }
    }
}
