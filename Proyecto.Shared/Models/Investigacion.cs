using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Proyecto.Shared.Models
{
    public class Investigacion


    {

        public int IdInv { get; set; }
        public string Cedula { get; set; } = string.Empty;

        public string NombreProyecto { get; set; } = string.Empty;

        public int TiempoMeses { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public byte[]? Pdf { get; set; }
        public string Carrera { get; set; } = string.Empty;
    }
}

