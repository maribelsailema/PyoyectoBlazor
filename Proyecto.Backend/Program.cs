using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Proyecto.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ?? Configurar DbContext
            builder.Services.AddDbContext<PlataformaDocenteContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB"))
            );

            // ?? Configurar JWT
            var jwtKey = builder.Configuration["Jwt:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // ?? Agregar servicios
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ?? Redirección a Swagger
            app.MapGet("/", (HttpContext context) =>
            {
                context.Response.Redirect("/swagger/index.html", permanent: false);
            });

            // ?? Middleware
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            // ?? Agrega estas dos líneas en orden
            app.UseAuthentication(); // <-- IMPORTANTE: primero
            app.UseAuthorization();  // <-- después

            app.MapControllers();

            app.Run();
        }
    }
}
