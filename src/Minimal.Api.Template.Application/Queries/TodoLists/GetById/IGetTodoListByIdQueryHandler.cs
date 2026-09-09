using Minimal.Api.Template.Application.Dtos.TodoLists;

namespace Minimal.Api.Template.Application.Queries.TodoLists.GetById;

/// <summary>
/// The get to do list by id query handler.
/// </summary>
/// <seealso cref="IQueryHandler{TIn,TOut}"/>
public interface IGetTodoListByIdQueryHandler : IQueryHandler<Guid, TodoListDto>;