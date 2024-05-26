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

            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    Title = "L40Z.CAT",
                    Description = "Una simple API para gestionar usuarios",
                    TermsOfService = new Uri("https://github.com/developer-laoz396/L40Z.CAT?tab=readme-ov-file#l40zcat"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "L40Z",
                        Email = "developer-laoz396",
                        Url = new Uri("https://github.com/developer-laoz396/L40Z.CAT")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/developer-laoz396/L40Z.CAT?tab=MIT-1-ov-file#")
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"L40Z.CAT API Versión {Assembly.GetExecutingAssembly().GetName().Version}");
                c.RoutePrefix = "swagger";
            });

            // Middleware para administración de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Middleware para añadir el usuario a la auditoría
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
