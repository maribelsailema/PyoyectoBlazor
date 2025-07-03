using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class EvaluacionResumenDto
    {
        public int IdEval { get; set; }
        public string Cedula { get; set; }
        public string Periodo { get; set; }
        public decimal PuntajeFinal { get; set; }
        public DateOnly FechaEvaluacion { get; set; }
    }
}
