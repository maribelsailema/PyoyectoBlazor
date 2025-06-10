
using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Models;

namespace Proyecto.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<PlataformaDocenteContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB"))
);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.MapGet("/", (HttpContext context) =>
            {
                context.Response.Redirect("/swagger/index.html", permanent: false);
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
