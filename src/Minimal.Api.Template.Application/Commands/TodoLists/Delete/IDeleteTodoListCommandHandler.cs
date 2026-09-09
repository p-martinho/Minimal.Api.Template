using Minimal.Api.Template.Application.Dtos.TodoLists;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Delete;

/// <summary>
/// The delete to do list command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IDeleteTodoListCommandHandler : ICommandHandler<Guid, TodoListDto>;