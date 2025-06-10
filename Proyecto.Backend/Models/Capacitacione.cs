using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Models;

public partial class Capacitacione
{
    public int IdCap { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreCurso { get; set; } = null!;

    public int DuracionHoras { get; set; }

    public DateOnly FechaInicio { get; set; }

    public byte[]? Pdf { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;
}
