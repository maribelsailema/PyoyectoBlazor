using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;

namespace Proyecto.Backend.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar DbContext
            builder.Services.AddDbContext<PlataformaDocenteContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB"))
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
            builder.Services.AddOpenApi(); // Solo si realmente lo usas
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
                app.MapOpenApi();
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            // Usar CORS
            app.UseCors();

            // Ya no se usa JWT
            // app.UseAuthentication(); <-- Eliminar
            // app.UseAuthorization(); <-- Eliminar si no proteges nada

            app.MapControllers();

            app.Run();
        }
    }
}
