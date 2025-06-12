using System.ComponentModel.DataAnnotations;

namespace Proyecto.Backend.Models
{
    public class LoginDTO
    {
        public string Usuari { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
}
