using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.Features;
#if IsToExcludeIdentity
using Microsoft.IdentityModel.Tokens;
#endif
using OpenIddict.Validation.AspNetCore;
using Serilog;
using Todo.Application.DependencyInjection;
using Todo.Common.Authorization;
#if IsToExcludeIdentity
using Todo.Common.Extensions;
#endif
using Todo.Presentation.Api.Middleware;
using Todo.Presentation.Api.Settings;

namespace Todo.Presentation.Api.DependencyInjection;

/// <summary>
/// The dependency injection extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class DependencyInjectionExtensions
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    /// <param name="services">The service collection.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the custom health checks dependencies.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddCustomHealthChecks(IConfiguration configuration)
        {
            // Add here specific health checks for this API. Default health checks were already registered in ServiceDefaults project.

            services.AddHealthChecks()
                .AddApplicationHealthChecks(configuration);

            return services;
        }

        /// <summary>
        /// Adds the API dependencies.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="hostEnvironment">The host environment.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddApiDependencies(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.AddApplication(configuration, hostEnvironment);

            services.AddHttpContextAccessor();

            services.AddSettings(configuration);

            services.AddProblemDetails();

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddAuthenticationAndAuthorization();

#if IsToExcludeIdentity
            services.AddOpenIddictValidation(configuration, hostEnvironment);

#endif
            services.AddApiVersioning();

            services.AddLogging(configuration);

            return services;
        }

        private void AddSettings(IConfiguration configuration)
        {
            // Throw exception on binding error (on non-Development) (the exception handler will handle it)
            services.Configure<RouteHandlerOptions>(options => { options.ThrowOnBadRequest = true; });

            services.Configure<InternalErrorMiddlewareSettings>(
                configuration.GetSection(nameof(InternalErrorMiddlewareSettings)));
        }

        private void AddProblemDetails()
        {
            services.AddProblemDetails(options =>
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance =
                        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                });
        }

        private void AddAuthenticationAndAuthorization()
        {
            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.HealthChecksFull,
                    policyBuilder => policyBuilder.RequireRole(UserRoles.Admin).Build());
            });
        }

#if IsToExcludeIdentity
        private void AddOpenIddictValidation(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the issuer signing keys used to validate tokens.
                    options.SetIssuer(configuration["IdentitySettings:Issuer"] ?? string.Empty);

                    options.AddAudiences(configuration["IdentitySettings:Audience"] ?? string.Empty);

                    if (!hostEnvironment.IsDevelopment() && !hostEnvironment.IsMigration())
                    {
                        // Register the encryption credentials.
                        options.AddEncryptionKey(new SymmetricSecurityKey(
                            Convert.FromBase64String(configuration["IdentitySettings:EncryptionKey"] ?? string.Empty)));
                    }

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
        }

#endif
        private void AddApiVersioning()
        {
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VV";
                });
        }

        private void AddLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(cfg => cfg.Console())
                .CreateLogger();

            services.AddSerilog(loggerConfig =>
            {
                loggerConfig.ReadFrom.Configuration(configuration);
                loggerConfig.WriteTo.OpenTelemetry();
            });
        }
    }
}