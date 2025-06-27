using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Shared.Models
{
    public class ObraS
    {
        [Key]
        public int IdObra { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El título de la obra es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El tipo de obra es obligatorio")]
        public string TipoObra { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        public DateTime FechaPublicacion { get; set; }

        public string Editorial { get; set; }
        public string ISBN { get; set; }
        public string DOI { get; set; }
        public byte[] Documento { get; set; }
        public string NombreArchivo { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria")]
        public int IdCarrera { get; set; }

        // Propiedad de navegación (opcional)
        public Carrera Carrera { get; set; }
    }
}