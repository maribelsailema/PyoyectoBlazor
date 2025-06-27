using System.Net.Http.Json;
using Proyecto.Shared.Models;

namespace Proyecto.Frontend.Services;

public class CapacitacionesService
{
    private readonly HttpClient _http;
    public CapacitacionesService(HttpClient http) => _http = http;

    public async Task<List<Capacitacione>> ObtenerPorCedulaAsync(string cedula)
    {
        var todas = await _http.GetFromJsonAsync<List<Capacitacione>>(
                        "api/Capacitaciones/Listar") ?? new();
        return todas.Where(c => c.Cedula == cedula).ToList();
    }

    public async Task<Capacitacione?> GuardarAsync(Capacitacione cap)
        => await (await _http.PostAsJsonAsync("api/Capacitaciones/Guardar", cap))
                   .Content.ReadFromJsonAsync<Capacitacione>();

    public Task ActualizarAsync(Capacitacione cap)
        => _http.PutAsJsonAsync($"api/Capacitaciones/Actualizar/{cap.IdCap}", cap);

    public Task EliminarAsync(int idCap)
        => _http.DeleteAsync($"api/Capacitaciones/Eliminar/{idCap}");

}
//capacitacion todo
