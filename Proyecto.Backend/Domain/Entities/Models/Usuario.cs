using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class Usuario
{
    public string Ced { get; set; } = string.Empty;

    public string Nom1 { get; set; } = null!;

    public string? Nom2 { get; set; }

    public string Ape1 { get; set; } = null!;

    public string? Ape2 { get; set; }

    public string Usuari { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public DateOnly? FechaIngreso { get; set; }

    public virtual ICollection<Capacitacione> Capacitaciones { get; set; } = new List<Capacitacione>();

    public virtual ICollection<EvaluacionesDocente> EvaluacionesDocentes { get; set; } = new List<EvaluacionesDocente>();

    public virtual ICollection<Investigacione> Investigaciones { get; set; } = new List<Investigacione>();

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();

    public virtual ICollection<RolesDocente> RolesDocentes { get; set; } = new List<RolesDocente>();
}
