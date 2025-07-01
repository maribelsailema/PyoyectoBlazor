using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

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

    public string? Tipo { get; set; }

    public string? Estado { get; set; }
    public string? Cientifico { get; set; }


    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Usuario? CedulaNavigation { get; set; }

    public virtual Carrera? IdCarreraNavigation { get; set; }
}
