using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class ResumenDocumentosDto
    {
        public int TotalObras { get; set; }
        public decimal? PuntajeEvaluacion { get; set; }
        public int TotalHorasCapacitacion { get; set; }
        public int TotalMesesInvestigacion { get; set; }
    }

}
