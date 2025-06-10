using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Models;

public partial class EvaluacionesDocente
{
    public int IdEval { get; set; }

    public string Cedula { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public decimal PuntajeFinal { get; set; }

    public DateOnly FechaEvaluacion { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;
}
