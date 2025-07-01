using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class ResumenPostulanteDto
    {
        public int CantidadObras { get; set; }
        public decimal? PuntajeEvaluacion { get; set; }
        public int DuracionHoras { get; set; }
        public int TiempoMeses { get; set; }
    }

}
