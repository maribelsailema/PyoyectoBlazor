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
            return await _httpClient.GetFromJsonAsync<List<ObraS>>($"api/Obra/buscar/{cedula}");
        }

        public async Task<ObraS> GuardarObraAsync(ObraS obra)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Obra", obra);
            return await response.Content.ReadFromJsonAsync<ObraS>();
        }

        public async Task ActualizarObraAsync(ObraS obra)
        {
            await _httpClient.PutAsJsonAsync($"api/Obra/{obra.IdObra}", obra);
        }

        public async Task EliminarObraAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Obra/{id}");
        }

        public async Task<List<Carrera>> ObtenerCarrerasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Carrera>>("api/Carrera");
        }
    }
}