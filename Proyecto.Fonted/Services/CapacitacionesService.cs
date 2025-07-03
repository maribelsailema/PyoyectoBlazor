using Proyecto.Shared.Dtos;
using Proyecto.Shared.Models;
using System.Net.Http.Json;

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
    private static CapacitacionCreateDto ToDto(Capacitacione c) => new()
    {
        Cedula = c.Cedula,
        NombreCurso = c.NombreCurso,
        DuracionHoras = c.DuracionHoras,
        FechaInicio = c.FechaInicio,
        FechaFin = c.FechaFin,
        TipoCapacitacion = c.TipoCapacitacion,
        Institucion = c.Institucion,
        Modalidad = c.Modalidad,
        Certificado = c.Certificado,
        Observaciones = c.Observaciones,
        Pdf = c.Pdf
    };


    public async Task<Capacitacione?> GuardarAsync(Capacitacione cap)
        => await (await _http.PostAsJsonAsync("api/Capacitaciones/GuardarDto", ToDto(cap)))
               .Content.ReadFromJsonAsync<Capacitacione>();

    public Task ActualizarAsync(Capacitacione cap)
        => _http.PutAsJsonAsync($"api/Capacitaciones/ActualizarDto/{cap.IdCap}", ToDto(cap));

    public Task EliminarAsync(int idCap)
        => _http.DeleteAsync($"api/Capacitaciones/Eliminar/{idCap}");

}
//capacitacion todo
