using System.Diagnostics.CodeAnalysis;

namespace Todo.Common.Authorization;

/// <summary>
/// The authorization policies.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Policies
{
    public const string HealthChecksFull = "HealthChecksFullPolicy";
}