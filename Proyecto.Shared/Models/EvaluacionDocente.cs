using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Shared.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EvaluacionDocente
    {
        public int IdEval { get; set; }
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El período es obligatorio")]
        public string Periodo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El puntaje debe ser un número positivo")]
        public decimal PuntajeFinal { get; set; }

        [Required(ErrorMessage = "La fecha de evaluación es obligatoria")]
        public DateTime FechaEvaluacion { get; set; }

        public byte[]? Pdf { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el tipo de evaluación.")]
        public string? TipoEvaluacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar el estado.")]
        public string? Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar el modo de evaluación.")]
        public string? ModoEvaluacion { get; set; } = string.Empty;
    }

}

