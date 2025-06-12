using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

public class AuthService : IAuthService
{
    private readonly PlataformaDocenteContext _context;
    private readonly IConfiguration _config;
    private readonly JwtHelper _jwtHelper;

    public AuthService(PlataformaDocenteContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _jwtHelper = new JwtHelper(config);
    }

    public LoginResponse? Login(LoginRequest request)
    {
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Usuari == request.Usuari && u.Pass == request.Pass);

        if (usuario == null)
            return null;

        var token = _jwtHelper.GenerateToken(usuario);

        return new LoginResponse
        {
            Token = token,
            TipoUsuario = usuario.TipoUsuario,
            Ced = usuario.Ced,
            NombreCompleto = $"{usuario.Nom1} {usuario.Ape1}"
        };
    }
}
