using Proyecto.Backend.Models;

public interface IAuthService
{
    LoginResponse? Login(LoginRequest request);
}
