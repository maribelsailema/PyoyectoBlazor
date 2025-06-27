using Proyecto.Backend.Domain.Entities.Models;
using System.Net.Http.Json;
using Proyecto.Shared.Models;

public class ObraService
{
    private readonly HttpClient _http;
    public ObraService(HttpClient http) => _http = http;

    public async Task<List<Obra>> ObtenerPorCedulaAsync(string cedula)
    {
        var resp = await _http.GetAsync($"api/Obras/buscar/{cedula}");
        return resp.IsSuccessStatusCode
            ? await resp.Content.ReadFromJsonAsync<List<Obra>>() ?? new List<Obra>()
            : new List<Obra>();
    }

    public async Task<Obra?> GuardarObraAsync(Obra obra)
    {
        var resp = await _http.PostAsJsonAsync("api/Obras", obra);
        return resp.IsSuccessStatusCode
            ? await resp.Content.ReadFromJsonAsync<Obra>()
            : null;
    }
}
