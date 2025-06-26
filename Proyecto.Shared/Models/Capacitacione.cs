using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto.Shared.Models
{
    public class Capacitacione
    {
        public int IdCap { get; set; }

        public string Cedula { get; set; } = string.Empty;

        public string NombreCurso { get; set; } = string.Empty;

        public int DuracionHoras { get; set; }

        public DateOnly FechaInicio { get; set; }

        public byte[]? Pdf { get; set; }
    }
}
