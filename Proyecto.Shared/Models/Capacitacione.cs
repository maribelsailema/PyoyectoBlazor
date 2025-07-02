using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;       // 👈  NUEVO using
namespace Proyecto.Shared.Models;

/// <summary>
/// DTO idéntico a la entidad EF, pero sin navigation properties para evitar ciclos JSON
/// </summary>
public class Capacitacione
{
    public int IdCap { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "La cédula debe tener 10 dígitos")]
    public string Cedula { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del curso es obligatorio")]
    [StringLength(150)]
    public string NombreCurso { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Las horas deben ser mayores que cero")]
    public int DuracionHoras { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
    public DateTime FechaInicio { get; set; }  // Cambia DateOnly por DateTime

    // 🔹 NUEVOS CAMPOS
    [StringLength(40)]
    public string? TipoCapacitacion { get; set; }

    [StringLength(150)]
    public string? Institucion { get; set; }

    [StringLength(10)]
    public string? Modalidad { get; set; }

    public bool Certificado { get; set; } = false;

    [StringLength(500)]
    public string? Observaciones { get; set; }

    public byte[]? Pdf { get; set; }


  
}

//capacticaion todo