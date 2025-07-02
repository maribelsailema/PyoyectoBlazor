using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Dtos;

public record CapacitacionCreateDto
{
    public string Cedula { get; init; } = string.Empty;
    public string NombreCurso { get; init; } = string.Empty;
    public int DuracionHoras { get; init; }
    public DateTime FechaInicio { get; init; }
    public string? TipoCapacitacion { get; init; }
    public string? Institucion { get; init; }
    public string? Modalidad { get; init; }
    public bool Certificado { get; init; } = false;
    public string? Observaciones { get; init; }// ← DateTime
    public byte[]? Pdf { get; init; }
}
