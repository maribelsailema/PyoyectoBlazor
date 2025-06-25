using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Proyecto.Shared.Models
{
    public class Investigacion
    {
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        public string NombreProyecto { get; set; } = string.Empty;

        public int TiempoMeses { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public byte[]? Pdf { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        public int? IdCarrera { get; set; }  // <-- Usamos IdCarrera ahora
        public string Carrera { get; set; } = string.Empty;

        public string? NombreCarrera { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public int IdInv { get; set; }

    }
}

