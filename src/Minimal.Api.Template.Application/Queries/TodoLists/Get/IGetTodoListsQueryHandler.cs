using Minimal.Api.Template.Application.Dtos;
using Minimal.Api.Template.Application.Dtos.TodoLists;

namespace Minimal.Api.Template.Application.Queries.TodoLists.Get;

/// <summary>
/// The get to do lists query handler.
/// </summary>
/// <seealso cref="IQueryHandler{TIn,TOut}"/>
public interface IGetTodoListsQueryHandler : IQueryHandler<PaginatedQueryDto, PaginatedQueryResultDto<TodoListDto>>;