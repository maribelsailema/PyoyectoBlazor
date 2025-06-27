using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Shared.Models
{
    public class UsuarioModel
    {
        public string Nom1 { get; set; } = string.Empty;
        public string Ape1 { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nom1} {Ape1}";
    }

}
