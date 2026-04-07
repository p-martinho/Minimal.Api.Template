using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.Common.HealthChecks;
using Todo.Persistence.Constants;
using Todo.Persistence.Interceptors;

namespace Todo.Persistence.DependencyInjection;

/// <summary>
/// The EF Core dependency injection extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class EfCoreDependencyInjectionExtensions
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    /// <param name="services">The service collection.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the EF Core dependencies.
        /// </summary>
        /// <typeparam name="TContext">The specific type of DB context.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddEfCore<TContext>(IConfiguration configuration) where TContext : DbContext
        {
            services.AddInterceptors();

            services.AddDbContext<TContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.AddDatabaseProvider(configuration);

                optionsBuilder.AddInterceptors(GetInterceptors(serviceProvider));
            });

            ApplyDatabaseMigrationsIfDevelopment<TContext>(services);

            return services;
        }

        private void AddInterceptors()
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, SoftDeletableEntityInterceptor>();
        }
    }

    /// <summary>
    /// The <see cref="IHealthChecksBuilder"/> extensions.
    /// </summary>
    /// <param name="healthChecksBuilder">The health checks builder.</param>
    extension(IHealthChecksBuilder healthChecksBuilder)
    {
        /// <summary>
        /// Adds the EF Core health checks.
        /// </summary>
        public IHealthChecksBuilder AddEfCoreHealthChecks<TContext>()
            where TContext : DbContext
        {
            healthChecksBuilder.AddDbContextCheck<TContext>(tags: [HealthChecksTags.DbContext]);

            return healthChecksBuilder;
        }
    }

    /// <summary>
    /// The <see cref="DbContextOptionsBuilder"/> extensions.
    /// </summary>
    /// <param name="optionsBuilder">The DB context options builder.</param>
    extension(DbContextOptionsBuilder optionsBuilder)
    {
        private void AddDatabaseProvider(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.SqlDefault);

            optionsBuilder
                .UseSqlServer(connectionString,
                    sqlOptionsBuilder =>
                        sqlOptionsBuilder.EnableRetryOnFailure()
                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        }
    }

    private static IEnumerable<IInterceptor> GetInterceptors(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetServices<ISaveChangesInterceptor>();
    }

    private static void ApplyDatabaseMigrationsIfDevelopment<TContext>(IServiceCollection services)
        where TContext : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();

        var isDevelopment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>().IsDevelopment();

        if (!isDevelopment)
        {
            return;
        }

        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        context.Database.Migrate();
    }
}