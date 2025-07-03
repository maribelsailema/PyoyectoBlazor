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
public class Capacitacione : IValidatableObject
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
    [Required(ErrorMessage = "La fecha de final  es obligatoria")]
    public DateTime? FechaFin { get; set; }      // ← MISMO TIPO (DateTime?)

    // 🔹 NUEVOS CAMPOS
    [StringLength(40)]
    public string? TipoCapacitacion { get; set; }

    [Required(ErrorMessage = "La institución es obligatoria")]   // ⬅️ REGLA 2
    [StringLength(150)]
    public string? Institucion { get; set; }

    [StringLength(10)]
    public string? Modalidad { get; set; }

    public bool Certificado { get; set; } = false;
    [Required(ErrorMessage = "Las Observaciiones  son  obligatoria")]   // ⬅️ REGLA 2
    [StringLength(500)]
    public string? Observaciones { get; set; }

    public byte[]? Pdf { get; set; }


    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        DateTime hoy = DateTime.Today;
        DateTime limiteInferior = hoy.AddYears(-4);

        // 1️⃣ FechaInicio debe estar en los últimos 4 años y no ser futura
        if (FechaInicio < limiteInferior || FechaInicio > hoy)
        {
            yield return new ValidationResult(
                $"La fecha de inicio debe estar entre {limiteInferior:dd/MM/yyyy} y {hoy:dd/MM/yyyy}.",
                new[] { nameof(FechaInicio) });
        }

        if (FechaFin.HasValue)
        {
            // 2️⃣ FechaFin no puede ser anterior a FechaInicio
            if (FechaFin.Value < FechaInicio)
            {
                yield return new ValidationResult(
                    "La fecha de fin no puede ser anterior a la fecha de inicio.",
                    new[] { nameof(FechaFin) });
            }

            // 3️⃣ FechaFin no puede ser posterior a hoy
            if (FechaFin.Value > hoy)
            {
                yield return new ValidationResult(
                    "La fecha de fin no puede ser posterior al día de hoy.",
                    new[] { nameof(FechaFin) });
            }
        }
    }
}

//capacticaion todo