using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto.Frontend.Services;
using System.Net.Http;
using Proyecto.Fonted; // o el namespace real donde esté App.razor



namespace Proyecto.Fonted
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Cambiar BaseAddress al URL del backend API
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7201/") });
            builder.Services.AddScoped<InvestigacionService>();
            builder.Services.AddScoped<CapacitacionesService>();

            await builder.Build().RunAsync();
        }
    }
}
