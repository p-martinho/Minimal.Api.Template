using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Common.DependencyInjection;
using Todo.Persistence.Repositories.Settings;
using Todo.Persistence.Repositories.TodoLists;

namespace Todo.Persistence.DependencyInjection;

/// <summary>
/// The dependency injection extensions.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    /// <param name="services">The service collection.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the persistence dependencies.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddPersistence(IConfiguration configuration)
        {
            services.AddCommon(configuration);
            
            services.AddEfCore<ApplicationDbContext>(configuration);

            services.AddRepositories();
            
            services.AddSettings(configuration);

            return services;
        }
        
        private void AddRepositories()
        {
            services.AddScoped<ITodoListRepository, TodoListRepository>();
        }
        
        private void AddSettings(IConfiguration configuration)
        {
            services.Configure<QueryParametersSettings>(configuration.GetSection(nameof(QueryParametersSettings)));
        }
    }

    /// <summary>
    /// The <see cref="IHealthChecksBuilder"/> extensions.
    /// </summary>
    /// <param name="healthChecksBuilder">The health checks builder.</param>
    extension(IHealthChecksBuilder healthChecksBuilder)
    {
        /// <summary>
        /// Adds the persistence health checks.
        /// </summary>
        /// <returns>The health checks builder.</returns>
        public IHealthChecksBuilder AddPersistenceHealthChecks()
        {
            healthChecksBuilder.AddEfCoreHealthChecks<ApplicationDbContext>();

            return healthChecksBuilder;
        }
    }
}