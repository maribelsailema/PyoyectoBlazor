using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class Capacitacione
{
    public int IdCap { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreCurso { get; set; } = null!;

    public int DuracionHoras { get; set; }

    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }

    public string? TipoCapacitacion { get; set; }   // Curso, Taller…
    public string? Institucion { get; set; }
    public string? Modalidad { get; set; }   // Presencial, Virtual…
    public bool Certificado { get; set; }   // 0 = No, 1 = Sí
    public string? Observaciones { get; set; }


    public byte[]? Pdf { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;
}
