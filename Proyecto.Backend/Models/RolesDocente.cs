using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Models;

public partial class RolesDocente
{
    public int IdRol { get; set; }

    public string Cedula { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public DateOnly FechaAsignacion { get; set; }

    public bool Activo { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;
}
