using Proyecto.Backend.Domain.Entities.Models;

public interface IAuthService
{
    LoginResponse? Login(LoginRequest request);
}
