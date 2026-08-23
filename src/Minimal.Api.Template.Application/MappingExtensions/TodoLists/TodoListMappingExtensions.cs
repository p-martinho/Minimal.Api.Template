using System.Diagnostics.CodeAnalysis;
using Minimal.Api.Template.Application.Dtos.TodoLists;
using Minimal.Api.Template.Domain.Entities.TodoLists;

namespace Minimal.Api.Template.Application.MappingExtensions.TodoLists;

/// <summary>
/// The to do list mapping extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class TodoListMappingExtensions
{
    /// <summary>
    /// The <see cref="TodoList"/> extensions.
    /// </summary>
    /// <param name="entity">The entity.</param>
    extension(TodoList entity)
    {
        /// <summary>
        /// Converts the entity into a DTO.
        /// </summary>
        /// <returns>The DTO.</returns>
        public TodoListDto ToDto()
        {
            return new TodoListDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Items = entity.Items.Select(i => i.ToDto()).ToList().AsReadOnly()
            };
        }
    }
}