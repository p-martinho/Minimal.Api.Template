using Minimal.Api.Template.Application.Dtos.Identity.Users;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Create;

namespace Minimal.Api.Template.Application.Commands.Identity.Users.Create;

/// <summary>
/// The create user command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface ICreateUserCommandHandler : ICommandHandler<CreateUserDto, UserInfoDto>;