using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class ObraResumenDto
    {
        public int IdObra { get; set; }
        public string Cedula { get; set; }
        public string TipoObra { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
