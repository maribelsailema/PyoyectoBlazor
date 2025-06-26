using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class RequisitoPromocionDto
    {
        public string DesdeRol { get; set; }
        public string HaciaRol { get; set; }
        public int TiempoMinimoAnios { get; set; }
        public int ObrasMinimas { get; set; }
        public decimal PuntajeEvaluacionMinimo { get; set; }
        public int HorasCapacitacionMin { get; set; }
        public int? MesesInvestigacionMin { get; set; }
        public bool CumpleRequisitos { get; set; }
    }
}
