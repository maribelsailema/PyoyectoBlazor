namespace Proyecto.Backend.Domain.Entities.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
        public string Ced { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
