using Minimal.Api.Template.Application.Dtos.TodoLists;
using Minimal.Api.Template.Application.Dtos.TodoLists.Create;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Create;

/// <summary>
/// The create to do list command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface ICreateTodoListCommandHandler : ICommandHandler<CreateTodoListDto, TodoListDto>;