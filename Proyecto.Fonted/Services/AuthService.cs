
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static Proyecto.Fonted.Pages.Registro;

namespace Proyecto.Fonted.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Register(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }
}
