using System.Diagnostics.CodeAnalysis;

namespace Todo.Common.Authorization;

/// <summary>
/// The JWT claims.
/// </summary>
[ExcludeFromCodeCoverage]
public static class JwtClaims
{
    /// <summary>
    /// The subject claim.
    /// </summary>
    public const string Subject = "sub";
}