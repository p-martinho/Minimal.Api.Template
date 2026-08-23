using Minimal.Api.Template.Domain.Entities.TodoLists;

namespace Minimal.Api.Template.Persistence.Repositories.TodoLists;

/// <summary>
/// The to do list repository.
/// </summary>
/// <seealso cref="IRepository{TEntity}"/>
public interface ITodoListRepository : IRepository<TodoList>;