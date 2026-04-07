using System.Diagnostics.CodeAnalysis;

namespace Todo.Persistence.Constants;

/// <summary>
/// The max length for different property types.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class MaxLength
{
    /// <summary>
    /// The max length for a Guid.
    /// </summary>
    public const int Guid = 36;
}