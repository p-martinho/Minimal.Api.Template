using System.Diagnostics.CodeAnalysis;

namespace Minimal.Api.Template.Persistence.Constants;

/// <summary>
/// The name for generic entity properties.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class EntityProperties
{
    /// <summary>
    /// The name for the "is deleted" property.
    /// </summary>
    public const string IsDeleted = "IsDeleted";
}