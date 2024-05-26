using Microsoft.EntityFrameworkCore;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Interfaces;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;

namespace CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
