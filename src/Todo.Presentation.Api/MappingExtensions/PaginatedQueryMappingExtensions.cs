using System.Diagnostics.CodeAnalysis;
using Todo.Application.Dtos;
using Todo.Presentation.Api.Dtos;

namespace Todo.Presentation.Api.MappingExtensions;

/// <summary>
/// The paginated query mapping extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class PaginatedQueryMappingExtensions
{
    /// <summary>
    /// The <see cref="PaginatedQueryApiDto"/> extensions.
    /// </summary>
    /// <param name="apiDto">The API DTO.</param>
    extension(PaginatedQueryApiDto apiDto)
    {
        /// <summary>
        /// Converts the API DTO into an application DTO.
        /// </summary>
        /// <returns>The application DTO.</returns>
        public PaginatedQueryDto ToDto()
        {
            return new PaginatedQueryDto
            {
                PageNumber = apiDto.PageNumber.GetValueOrDefault(),
                PageSize = apiDto.PageSize.GetValueOrDefault(),
                OrderBy = apiDto.OrderBy,
                IsDescendingOrder = apiDto.IsDescendingOrder.GetValueOrDefault(),
                FilterBy = apiDto.FilterBy,
                FilterValue = apiDto.FilterValue
            };
        }
    }
}