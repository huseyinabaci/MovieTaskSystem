using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Common.Helper;
using TaskSystem.Domain;
using TaskSystem.Infrastructure.Repository;
using TaskSystem.Infrastructure.Repository.Interfaces;

namespace TaskSystem.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring dependency injection in the application.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Registers project-specific dependencies into the service collection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            // Registers the service response helper for handling standardized responses.
            services.AddScoped<IServiceResponseHelper, ServiceResponseHelper>();

            // Registers the repository for managing movie entities.
            services.AddScoped<IRepository<Movie>, Repository<Movie>>();

            // Registers the repository for managing director entities.
            services.AddScoped<IRepository<Director>, Repository<Director>>();

            return services;
        }
    }
}