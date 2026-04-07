using System.Diagnostics.CodeAnalysis;
using Todo.Presentation.Api.Middleware;
using Todo.Presentation.Api.Settings;

namespace Todo.Presentation.Api.Extensions;

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
    }

    private static bool IsInternalErrorMiddlewareEnabled(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(InternalErrorMiddlewareSettings))
            .Get<InternalErrorMiddlewareSettings>();

        return settings?.IsEnabled ?? false;
    }
}