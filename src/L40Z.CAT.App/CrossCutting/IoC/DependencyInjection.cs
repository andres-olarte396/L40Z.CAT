using Microsoft.EntityFrameworkCore;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Interfaces;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;

namespace CrossCutting.IoC
{
    /// <summary>
    /// Dependency injection class
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add project dependencies
        /// </summary>
        /// <param name="services">
        /// The services
        /// </param>
        /// <param name="connectionString">
        /// The connection string
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>
        /// </returns>
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
