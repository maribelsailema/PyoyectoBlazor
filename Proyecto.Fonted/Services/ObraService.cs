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
            try
            {
                Console.WriteLine($"Intentando obtener obras para cédula: {cedula}");
                var response = await _httpClient.GetAsync($"api/Obra/PorCedula/{cedula}");

                if (response.IsSuccessStatusCode)
                {
                    var obras = await response.Content.ReadFromJsonAsync<List<ObraS>>();
                    Console.WriteLine($"Obras obtenidas: {obras?.Count ?? 0}");
                    return obras ?? new List<ObraS>();
                }

                Console.WriteLine($"Error al obtener obras: {response.StatusCode}");
                return new List<ObraS>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al obtener obras: {ex.Message}");
                return new List<ObraS>();
            }
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

        public async Task<List<ObraS>> ListarObrasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ObraS>>("api/Obra/Listar")
                ?? new List<ObraS>();
        }

        public async Task<ObraS?> BuscarObraAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ObraS>($"api/Obra/Buscar/{id}");
        }
    }
}
