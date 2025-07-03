using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class InvestigacionResumenDto
    {
        public int IdInv { get; set; }
        public string Cedula { get; set; }
        public string NombreProyecto { get; set; }
        public int TiempoMeses { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Cientifico { get; set; }
    }
}
