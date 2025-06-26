using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class Obra
{
    public int IdObra { get; set; }

    public string Cedula { get; set; } = null!;

    public string TipoObra { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public byte[]? Pdf { get; set; }
}
