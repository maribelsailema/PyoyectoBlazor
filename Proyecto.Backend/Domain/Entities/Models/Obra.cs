using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class Obra
{
    public int IdObra { get; set; }

    public string Cedula { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public string TipoObra { get; set; } = string.Empty;

    public DateOnly Fecha { get; set; }

    public string? Pais { get; set; }
    public string? Ciudad { get; set; }
    public string? Editorial { get; set; }
    public string? ISBN { get; set; }
    public string? DOI { get; set; }
    public string? Enlace { get; set; }
    public string? Autores { get; set; }
    public string? Resumen { get; set; }
    public byte[]? Documento { get; set; }
    public virtual Usuario CedulaNavigation { get; set; } = null!;
}