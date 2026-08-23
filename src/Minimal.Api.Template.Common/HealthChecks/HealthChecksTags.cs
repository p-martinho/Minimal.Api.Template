using System.Diagnostics.CodeAnalysis;

namespace Minimal.Api.Template.Common.HealthChecks;

/// <summary>
/// The health checks tags.
/// </summary>
[ExcludeFromCodeCoverage]
public static class HealthChecksTags
{
    /// <summary>
    /// The alive tag.
    /// </summary>
    public const string Live = "Live";

    /// <summary>
    /// The DB context tag.
    /// </summary>
    public const string DbContext = "DbContext";
}