using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Shared.Models
{
    public class ObraS
    {
        [Key]
        public int IdObra { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; } = string.Empty;


        [Required(ErrorMessage = "El título de la obra es obligatorio")]
        public string Titulo { get; set; }= string.Empty;

        [Required(ErrorMessage = "El tipo de obra es obligatorio")]
        public string TipoObra { get; set; }= string.Empty;

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        public DateTime FechaPublicacion { get; set; }
        public byte[] Documento { get; set; }
        public string NombreArchivo { get; set; }

    }
}