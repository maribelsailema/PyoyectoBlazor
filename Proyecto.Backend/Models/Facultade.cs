using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Models;

public partial class Facultade
{
    public int IdFacultad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();
}
