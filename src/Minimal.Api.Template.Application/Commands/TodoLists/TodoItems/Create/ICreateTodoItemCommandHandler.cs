using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems.Create;

namespace Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Create;

/// <summary>
/// The create to do item command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface ICreateTodoItemCommandHandler : ICommandHandler<CreateTodoItemDto, TodoItemDto>;