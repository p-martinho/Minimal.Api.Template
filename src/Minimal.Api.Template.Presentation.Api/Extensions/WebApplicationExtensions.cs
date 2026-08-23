using System.Diagnostics.CodeAnalysis;
using Minimal.Api.Template.Presentation.Api.Middleware;
using Minimal.Api.Template.Presentation.Api.Settings;
#if (!IsToExcludeIdentity)
using Minimal.Api.Template.Application.Commands.Identity.OpenId.Seed;
using Minimal.Api.Template.Application.Commands.Identity.Roles.Seed;
using Minimal.Api.Template.Application.Commands.Models;
#endif

namespace Minimal.Api.Template.Presentation.Api.Extensions;

/// <summary>
/// The Web application extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class WebApplicationExtensions
{
    /// <summary>
    /// The <see cref="WebApplication"/> extensions.
    /// </summary>
    /// <param name="app">The Web application.</param>
    extension(WebApplication app)
    {
        /// <summary>
        /// Adds the internal error middleware into the Web application.
        /// </summary>
        /// <returns>The Web application.</returns>
        public WebApplication UseInternalErrorMiddleware()
        {
            if (IsInternalErrorMiddlewareEnabled(app.Configuration))
            {
                app.UseMiddleware<InternalErrorMiddleware>();
            }

            return app;
        }

#if (!IsToExcludeIdentity)
        /// <summary>
        /// Seeds the required resources asynchronous.
        /// </summary>
        /// <returns>The task.</returns>
        public async Task SeedResourcesAsync()
        {
            await using var scope = app.Services.CreateAsyncScope();

            if (app.Environment.IsDevelopment())
            {
                // Seed the OpenId resources in Development.
                await SeedOpenIdResourcesAsync(scope);
            }

            await SeedUserRolesAsync(scope);
        }
#endif
    }

    private static bool IsInternalErrorMiddlewareEnabled(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(InternalErrorMiddlewareSettings))
            .Get<InternalErrorMiddlewareSettings>();

        return settings?.IsEnabled ?? false;
    }

#if (!IsToExcludeIdentity)
    private static Task<CommandOut<bool>> SeedOpenIdResourcesAsync(AsyncServiceScope scope)
    {
        var seedHandler = scope.ServiceProvider.GetRequiredService<ISeedOpenIdTestingResourcesCommandHandler>();

        return seedHandler.HandleAsync();
    }

    private static Task<CommandOut<bool>> SeedUserRolesAsync(AsyncServiceScope scope)
    {
        var seedHandler = scope.ServiceProvider.GetRequiredService<ISeedRolesCommandHandler>();

        return seedHandler.HandleAsync();
    }
#endif
}