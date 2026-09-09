using Minimal.Api.Template.Application.Dtos.TodoLists;
using Minimal.Api.Template.Application.Dtos.TodoLists.Update;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Update;

/// <summary>
/// The update to do list command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IUpdateTodoListCommandHandler : ICommandHandler<UpdateTodoListDto, TodoListDto>;