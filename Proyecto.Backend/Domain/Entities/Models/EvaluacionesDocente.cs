using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class EvaluacionesDocente
{
    public int IdEval { get; set; }

    public string Cedula { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public decimal PuntajeFinal { get; set; }

    public DateOnly FechaEvaluacion { get; set; }

    public virtual Usuario CedulaNavigation { get; set; } = null!;

    public byte[]? Pdf { get; set; }

    [Required(ErrorMessage = "Debe seleccionar el tipo de evaluación.")]
    public string? TipoEvaluacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar el estado.")]
    public string? Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar el modo de evaluación.")]
    public string? ModoEvaluacion { get; set; } = string.Empty;

}
