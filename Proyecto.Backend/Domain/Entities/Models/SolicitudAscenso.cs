using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Backend.Domain.Entities.Models;

[Table("SolicitudesAscenso")]
public class SolicitudAscenso
{
    [Key]
    public int IdSolicitud { get; set; }

    [Required]
    [StringLength(10)]
    public string Cedula { get; set; } = null!;

    [Required]
    [StringLength(10)]
    public string RolActual { get; set; } = null!;

    [Required]
    [StringLength(10)]
    public string RolDeseado { get; set; } = null!;

    public DateTime FechaSolicitud { get; set; } = DateTime.Now;

    [Required]
    [StringLength(20)]
    public string Estado { get; set; } = "En espera";

    public string? Comentarios { get; set; }

    // RELACIÓN con Usuarios (opcional si quieres mostrar nombre)
    [ForeignKey("Cedula")]
    public virtual Usuario? Usuario { get; set; }
}
