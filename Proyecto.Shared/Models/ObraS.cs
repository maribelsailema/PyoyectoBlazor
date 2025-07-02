using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Shared.Models
{
    public class ObraS
    {
        [Key]
        public int IdObra { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(10)]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El título de la obra es obligatorio")]
        [StringLength(255)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de obra es obligatorio")]
        [StringLength(50)]
        public string TipoObra { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [StringLength(100)]
        public string? Pais { get; set; }

        [StringLength(100)]
        public string? Ciudad { get; set; }

        [StringLength(255)]
        public string? Editorial { get; set; }

        [StringLength(50)]
        public string? ISBN { get; set; }

        [StringLength(100)]
        public string? DOI { get; set; }

        [StringLength(255)]
        public string? Enlace { get; set; }

        [StringLength(255)]
        public string? Autores { get; set; }

        public string? Resumen { get; set; }

        public byte[]? PDF { get; set; }
    }
}