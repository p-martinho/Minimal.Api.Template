using System.Diagnostics.CodeAnalysis;

namespace Minimal.Api.Template.Presentation.Api.Settings;

/// <summary>
/// The internal error middleware settings.
/// </summary>
[ExcludeFromCodeCoverage]
internal class InternalErrorMiddlewareSettings
{
    /// <summary>
    /// Value indicating whether the middleware is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// The max length of the body to log when an internal error happens. If the length of the request body is more than this, the body will be truncated in the log.
    /// </summary>
    public int MaxBodyLengthInLog { get; set; } = 1024;
}