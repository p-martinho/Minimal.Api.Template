using System.Diagnostics.CodeAnalysis;
using Minimal.Api.Template.Application.Dtos;
using Minimal.Api.Template.Presentation.Api.Dtos;

namespace Minimal.Api.Template.Presentation.Api.MappingExtensions;

/// <summary>
/// The query result pagination mapping extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class QueryResultPaginationMappingExtensions
{
    /// <summary>
    /// The <see cref="QueryResultPaginationDto"/> extensions.
    /// </summary>
    /// <param name="dto">The application DTO.</param>
    extension(QueryResultPaginationDto dto)
    {
        /// <summary>
        /// Converts the application DTO into an API DTO.
        /// </summary>
        /// <returns>The API DTO.</returns>
        public QueryResultPaginationApiDto ToApiDto()
        {
            return new QueryResultPaginationApiDto
            {
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                PageRecords = dto.PageRecords,
                TotalRecords = dto.TotalRecords,
                TotalPages = dto.TotalPages,
                HasNextPage = dto.HasNextPage,
                HasPreviousPage = dto.HasPreviousPage
            };
        }
    }
}