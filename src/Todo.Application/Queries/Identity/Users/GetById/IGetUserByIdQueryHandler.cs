using Todo.Application.Dtos.Identity.Users;

namespace Todo.Application.Queries.Identity.Users.GetById;

/// <summary>
/// The get user by id query handler.
/// </summary>
/// <seealso cref="IQueryHandler{TIn,TOut}"/>
public interface IGetUserByIdQueryHandler : IQueryHandler<string, UserInfoDto>;