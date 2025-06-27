namespace Proyecto.Backend.Domain.Entities.Models
{
    public class Postulacion
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string RolActual { get; set; }
        public string RolSolicitado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }

}
