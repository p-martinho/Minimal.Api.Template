using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems.Delete;

namespace Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Delete;

/// <summary>
/// The delete to do item command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IDeleteTodoItemCommandHandler : ICommandHandler<DeleteTodoItemDto, TodoItemDto>;