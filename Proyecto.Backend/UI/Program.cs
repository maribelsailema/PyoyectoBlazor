using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Configurar DbContext para MySQL
            builder.Services.AddDbContext<PlataformaDocenteContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("connectionDB"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("connectionDB"))
                )
            );

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Agregar servicios
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Redirección a Swagger
            app.MapGet("/", (HttpContext context) =>
            {
                context.Response.Redirect("/swagger/index.html", permanent: false);
            });

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}
