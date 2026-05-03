using Todo.Application.Dtos.Identity.Users;
using Todo.Application.Dtos.Identity.Users.Update;

namespace Todo.Application.Commands.Identity.Users.Update;

/// <summary>
/// The update user info command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IUpdateUserInfoCommandHandler : ICommandHandler<UpdateUserInfoDto, UserInfoDto>;