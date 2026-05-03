using Todo.Application.Dtos.Identity.Users;
using Todo.Application.Dtos.Identity.Users.Create;

namespace Todo.Application.Commands.Identity.Users.Create;

/// <summary>
/// The create user command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface ICreateUserCommandHandler : ICommandHandler<CreateUserDto, UserInfoDto>;