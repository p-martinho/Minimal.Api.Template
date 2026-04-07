using System.Diagnostics.CodeAnalysis;

namespace Todo.Persistence.Repositories.Settings;

/// <summary>
/// The query parameters settings.
/// </summary>
[ExcludeFromCodeCoverage]
internal class QueryParametersSettings
{
    /// <summary>
    /// The maximum number of records a query can retrieve.
    /// </summary>
    public int MaxLimit { get; set; } = 100;
}