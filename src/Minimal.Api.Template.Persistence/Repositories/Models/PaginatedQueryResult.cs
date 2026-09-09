using System.Diagnostics.CodeAnalysis;
using Minimal.Api.Template.Domain.Abstractions;
using Minimal.Api.Template.Domain.Entities;

namespace Minimal.Api.Template.Persistence.Repositories.Models;

/// <summary>
/// The result of the paginated query.
/// </summary>
[ExcludeFromCodeCoverage]
public class PaginatedQueryResult<TEntity> where TEntity : BaseEntity, IAggregateEntity
{
    /// <summary>
    /// The collection of records.
    /// </summary>
    public IEnumerable<TEntity> Records { get; init; } = [];

    /// <summary>
    /// The query pagination.
    /// </summary>
    public QueryResultPagination Pagination { get; init; } = new();
}