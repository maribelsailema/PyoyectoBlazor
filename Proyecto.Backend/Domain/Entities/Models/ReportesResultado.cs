namespace Proyecto.Backend.Domain.Entities.Models;

public class ReportesResultado
{
    public string Docente { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string ValorPuntaje { get; set; } = string.Empty;
}
