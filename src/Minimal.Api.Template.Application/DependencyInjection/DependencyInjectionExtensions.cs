using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minimal.Api.Template.Application.Commands.TodoLists.Create;
using Minimal.Api.Template.Application.Commands.TodoLists.Delete;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Create;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Delete;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Update;
using Minimal.Api.Template.Application.Commands.TodoLists.Update;
using Minimal.Api.Template.Application.Dtos.TodoLists.Create;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems.Create;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems.Update;
using Minimal.Api.Template.Application.Dtos.TodoLists.Update;
using Minimal.Api.Template.Application.Queries.TodoLists.Get;
using Minimal.Api.Template.Application.Queries.TodoLists.GetById;
using Minimal.Api.Template.Persistence.DependencyInjection;
using Quartz;
#if (!IsToExcludeIdentity)
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Minimal.Api.Template.Application.Commands.Identity.OpenId.Seed;
using Minimal.Api.Template.Application.Commands.Identity.Roles.Seed;
using Minimal.Api.Template.Application.Commands.Identity.Tokens.Exchange;
using Minimal.Api.Template.Application.Commands.Identity.Users.Create;
using Minimal.Api.Template.Application.Commands.Identity.Users.Update;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Create;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Update;
using Minimal.Api.Template.Application.Queries.Identity.Users.GetById;
using Minimal.Api.Template.Common.Extensions;
using Minimal.Api.Template.Domain.Entities.Identity.Users;
#endif

namespace Minimal.Api.Template.Application.DependencyInjection;

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
        /// Adds the application dependencies.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="hostEnvironment">The host environment.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddApplication(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            services.AddPersistence(configuration);

            services.AddValidators();

            services.AddCommandHandlers();

            services.AddQueryHandlers();

#if (!IsToExcludeIdentity)
            services.AddIdentity();

            services.AddOpenIddictServer(configuration, hostEnvironment);

            services.AddIdentityValidators();

            services.AddIdentityCommandHandlers();

            services.AddIdentityQueryHandlers();

#endif
            return services;
        }

        private void AddValidators()
        {
            services.AddScoped<IValidator<CreateTodoListDto>, CreateTodoListValidator>();
            services.AddScoped<IValidator<UpdateTodoListDto>, UpdateTodoListValidator>();

            services.AddScoped<IValidator<CreateTodoItemDto>, CreateTodoItemValidator>();
            services.AddScoped<IValidator<UpdateTodoItemDto>, UpdateTodoItemValidator>();
        }

#if (!IsToExcludeIdentity)
        private void AddIdentity()
        {
            services.AddIdentityCore<AppIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddIdentityStore();
        }

        private void AddOpenIddictServer(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
            // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
            services.AddQuartz(options =>
            {
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();
            });

            // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.AddOpenIddict()
                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Enable Quartz.NET integration.
                    options.UseQuartz();

                    // Store is configured in the Persistence layer.
                    options.AddOpenIddictStore();
                })
                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    // Enable the flows.
                    options.AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    // Enable the endpoints.
                    options.SetTokenEndpointUris("connect/token");

                    // Register the signing and encryption credentials.
                    if (hostEnvironment.IsDevelopment() || hostEnvironment.IsMigration())
                    {
                        options.AddDevelopmentEncryptionCertificate()
                            .DisableAccessTokenEncryption();

                        options.AddDevelopmentSigningCertificate();
                    }
                    else
                    {
                        options.AddEncryptionKey(new SymmetricSecurityKey(
                            Convert.FromBase64String(configuration["IdentitySettings:EncryptionKey"] ?? string.Empty)));

                        options.AddSigningCertificate(configuration["IdentitySettings:SigningCertificateThumbprint"] ??
                                                      string.Empty);
                    }

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    var aspNetOptions = options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough();

                    // Disable HTTPS requirement (e.g. useful in local docker compose).
                    if (hostEnvironment.IsDevelopment() &&
                        configuration.GetSection("IdentitySettings:DisableHttps").Get<bool>())
                    {
                        aspNetOptions.DisableTransportSecurityRequirement();
                    }
                })
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    options.AddAudiences(configuration["IdentitySettings:Audience"] ?? string.Empty);

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
        }

        private void AddIdentityValidators()
        {
            services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserInfoDto>, UpdateUserInfoValidator>();
            services.AddScoped<IValidator<UpdateUserPasswordDto>, UpdateUserPasswordValidator>();
        }

        private void AddIdentityCommandHandlers()
        {
            services.AddScoped<ISeedOpenIdTestingResourcesCommandHandler, SeedOpenIdTestingResourcesCommandHandler>();
            services.AddScoped<ISeedRolesCommandHandler, SeedRolesCommandHandler>();

            services.AddScoped<IExchangeTokenCommandHandler, ExchangeTokenCommandHandler>();

            services.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
            services.AddScoped<IUpdateUserInfoCommandHandler, UpdateUserInfoCommandHandler>();
            services.AddScoped<IUpdateUserPasswordCommandHandler, UpdateUserPasswordCommandHandler>();
        }

        private void AddIdentityQueryHandlers()
        {
            services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
        }

#endif
        private void AddCommandHandlers()
        {
            services.AddScoped<ICreateTodoListCommandHandler, CreateTodoListCommandHandler>();
            services.AddScoped<IUpdateTodoListCommandHandler, UpdateTodoListCommandHandler>();
            services.AddScoped<IDeleteTodoListCommandHandler, DeleteTodoListCommandHandler>();

            services.AddScoped<ICreateTodoItemCommandHandler, CreateTodoItemCommandHandler>();
            services.AddScoped<IUpdateTodoItemCommandHandler, UpdateTodoItemCommandHandler>();
            services.AddScoped<IDeleteTodoItemCommandHandler, DeleteTodoItemCommandHandler>();
        }

        private void AddQueryHandlers()
        {
            services.AddScoped<IGetTodoListByIdQueryHandler, GetTodoListByIdQueryHandler>();
            services.AddScoped<IGetTodoListsQueryHandler, GetTodoListsQueryHandler>();
        }
    }

    /// <summary>
    /// The <see cref="IHealthChecksBuilder"/> extensions.
    /// </summary>
    /// <param name="healthChecksBuilder">The health checks builder.</param>
    extension(IHealthChecksBuilder healthChecksBuilder)
    {
        /// <summary>
        /// Adds the application health checks.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The health checks builder.</returns>
        public IHealthChecksBuilder AddApplicationHealthChecks(IConfiguration configuration)
        {
            healthChecksBuilder.AddPersistenceHealthChecks();

            return healthChecksBuilder;
        }
    }
}