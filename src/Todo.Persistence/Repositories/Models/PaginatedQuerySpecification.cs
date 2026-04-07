using System.Diagnostics.CodeAnalysis;
using Todo.Domain.Abstractions;
using Todo.Domain.Entities;

namespace Todo.Persistence.Repositories.Models;

/// <summary>
/// The paginated query specification.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <seealso cref="QuerySpecification{TEntity}"/>
[ExcludeFromCodeCoverage]
public class PaginatedQuerySpecification<TEntity> : QuerySpecification<TEntity>
    where TEntity : BaseEntity, IAggregateEntity
{
    /// <summary>
    /// The page number.
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// The page size.
    /// </summary>
    public int PageSize { get; init; }
}