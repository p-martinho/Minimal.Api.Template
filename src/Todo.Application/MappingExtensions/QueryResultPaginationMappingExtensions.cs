using System.Diagnostics.CodeAnalysis;
using Todo.Application.Dtos;
using Todo.Persistence.Repositories.Models;

namespace Todo.Application.MappingExtensions;

/// <summary>
/// The query result pagination mapping extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class QueryResultPaginationMappingExtensions
{
    /// <summary>
    /// The <see cref="QueryResultPagination"/> extensions.
    /// </summary>
    /// <param name="queryResultPagination">The query result pagination.</param>
    extension(QueryResultPagination queryResultPagination)
    {
        /// <summary>
        /// Converts the query result pagination into a DTO.
        /// </summary>
        /// <returns>The DTO.</returns>
        public QueryResultPaginationDto ToDto()
        {
            return new QueryResultPaginationDto
            {
                PageNumber = queryResultPagination.PageNumber,
                PageSize = queryResultPagination.PageSize,
                PageRecords = queryResultPagination.PageRecords,
                TotalRecords = queryResultPagination.TotalRecords
            };
        }
    }
}