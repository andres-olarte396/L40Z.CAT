using CrossCutting.IoC;
using CrossCutting.Middleware;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddProjectDependencies(Configuration.GetConnectionString("DefaultConnection"));

            // Configuraci�n de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Mi Proyecto API",
                    Description = "Una simple API para gestionar usuarios",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Tu Nombre",
                        Email = "tuemail@example.com",
                        Url = new Uri("https://twitter.com/tuusuario")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Usar bajo LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Si deseas incluir comentarios XML para documentar tu API
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            // Habilitar middleware para servir el documento JSON generado por Swagger y la interfaz de Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Proyecto API v1");
                c.RoutePrefix = string.Empty; // Para hacer que Swagger UI est� en la ra�z (http://localhost:<puerto>/)
            });

            // Middleware para administraci�n de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Middleware para a�adir el usuario a la auditor�a
            app.Use(async (context, next) =>
            {
                var user = context.User?.Identity?.Name ?? "system"; // Obtener el usuario autenticado
                context.Items["User"] = user;
                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
