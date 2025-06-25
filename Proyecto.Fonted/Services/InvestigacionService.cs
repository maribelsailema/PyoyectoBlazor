using System.Net.Http;
using System.Net.Http.Json;
using Proyecto.Shared.Models;

namespace Proyecto.Frontend.Services
{
    public class InvestigacionService
    {
        private readonly HttpClient _http;

        public InvestigacionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Investigacion>> ObtenerInvestigacionesAsync()
        {
            return await _http.GetFromJsonAsync<List<Investigacion>>("api/Investigaciones/Listar")
                   ?? new List<Investigacion>();
        }

        public async Task<List<Investigacion>> ObtenerPorCedulaAsync(string cedula)
        {
            return await _http.GetFromJsonAsync<List<Investigacion>>($"api/Investigaciones/PorCedula/{cedula}")
                   ?? new List<Investigacion>();
        }

        public async Task GuardarInvestigacionAsync(Investigacion investigacion)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Investigaciones/Guardar", investigacion);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Contenido del error: " + errorContent); // 🔍 Debug
                    throw new Exception($"Error al guardar la investigación: {errorContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Error de red o conexión: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw; // No volver a encapsular, para evitar mensajes repetidos
            }
        }

        public async Task ActualizarInvestigacionAsync(Investigacion inv)
        {


            try
            {
                var url = $"api/Investigaciones/Actualizar/{inv.IdInv}";
                var response = await _http.PutAsJsonAsync(url, inv);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar: {error}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Error de red o conexión: {httpEx.Message}");
            }
        }




    }
}


