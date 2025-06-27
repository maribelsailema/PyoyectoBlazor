using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class PostulacionDto
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string RolActual { get; set; }
        public string RolSolicitado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
    }



}
