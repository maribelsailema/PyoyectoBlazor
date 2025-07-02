using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class ResumenCompletoPostulanteDto
    {
        public List<ObraResumenDto> Obras { get; set; }
        public List<EvaluacionResumenDto> Evaluaciones { get; set; }
        public List<CapacitacionResumenDto> Capacitaciones { get; set; }
        public List<InvestigacionResumenDto> Investigaciones { get; set; }
    }
}
