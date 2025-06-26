using System;
using System.Collections.Generic;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class RequisitosAscenso
{
    public int Id { get; set; }

    public string DesdeRol { get; set; } = null!;

    public string HaciaRol { get; set; } = null!;

    public int TiempoMinimoAnios { get; set; }

    public int ObrasMinimas { get; set; }

    public decimal PuntajeEvaluacionMinimo { get; set; }

    public int HorasCapacitacionMin { get; set; }

    public int? MesesInvestigacionMin { get; set; }
}
