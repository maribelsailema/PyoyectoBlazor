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
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de obra es obligatorio")]
        public string TipoObra { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        public DateOnly Fecha { get; set; }

        public string? Pais { get; set; }
        public string? Ciudad { get; set; }
        public string? Editorial { get; set; }
        public string? ISBN { get; set; }
        public string? DOI { get; set; }
        public string? Enlace { get; set; }
        public string? Autores { get; set; }
        public string? Resumen { get; set; }
        public byte[]? Documento { get; set; }
    }
}