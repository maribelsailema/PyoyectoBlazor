using Microsoft.AspNetCore.Mvc;
using Proyecto.Backend.Domain.Entities.Models;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var response = _authService.Login(request);

        if (response == null)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(response);
    }
}
