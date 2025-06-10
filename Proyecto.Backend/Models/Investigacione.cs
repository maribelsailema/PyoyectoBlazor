using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Models;

public partial class Investigacione
{
    public int IdInv { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreProyecto { get; set; } = null!;

    public int TiempoMeses { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public byte[]? Pdf { get; set; }

    public int? IdCarrera { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;

    public virtual Carrera? IdCarreraNavigation { get; set; }
}
