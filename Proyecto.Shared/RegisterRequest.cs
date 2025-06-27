using System.ComponentModel.DataAnnotations;

namespace Proyecto.Shared
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "Debe tener mínimo 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme su contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
