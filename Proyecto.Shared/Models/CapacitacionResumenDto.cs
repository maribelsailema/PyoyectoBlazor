using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Proyecto.Shared.Models
    {
        public class CapacitacionResumenDto
        {
            public int IdCap { get; set; }
            public string Cedula { get; set; }
            public string NombreCurso { get; set; }
            public int DuracionHoras { get; set; }
            public DateOnly FechaInicio { get; set; }
        }
    

}
