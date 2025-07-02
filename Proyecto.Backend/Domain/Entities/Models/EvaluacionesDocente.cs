using System;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class EvaluacionesDocente
{
    public int IdEval { get; set; }

    public string Cedula { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public decimal PuntajeFinal { get; set; }

    public DateOnly FechaEvaluacion { get; set; }

    public byte[]? Pdf { get; set; }  // PDF en binario (VARBINARY)

    public virtual Usuario CedulaNavigation { get; set; } = null!;
}
