using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class Carrera
{
    public int IdCarrera { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public virtual Facultade IdFacultadNavigation { get; set; } = null!;

    public virtual ICollection<Investigacione> Investigaciones { get; set; } = new List<Investigacione>();
}
