using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems.Update;

namespace Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Update;

/// <summary>
/// The update to do item command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IUpdateTodoItemCommandHandler : ICommandHandler<UpdateTodoItemDto, TodoItemDto>;