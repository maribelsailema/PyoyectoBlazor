using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public class Obra
{
    public int IdObra { get; set; }
    public string Cedula { get; set; }
    public string Titulo { get; set; }
    public string TipoObra { get; set; }
    public DateTime Fecha { get; set; }
    public string? Pais { get; set; }
    public string? Ciudad { get; set; }
    public string? Editorial { get; set; }
    public string? ISBN { get; set; }
    public string? DOI { get; set; }
    public string? Enlace { get; set; }
    public string? Autores { get; set; }
    public string? Resumen { get; set; }
    public byte[]? Pdf { get; set; } // Ensure this property exists and matches the expected type  
    public virtual Usuario CedulaNavigation { get; set; }
}
