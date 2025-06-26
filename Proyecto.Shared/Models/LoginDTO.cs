using System.ComponentModel.DataAnnotations;

namespace Proyecto.Backend.Domain.Entities.Models
{
    public class LoginDTO
    {
        public string Usuari { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
}
