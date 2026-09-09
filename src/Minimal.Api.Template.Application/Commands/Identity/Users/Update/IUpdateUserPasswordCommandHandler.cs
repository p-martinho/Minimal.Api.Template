using Minimal.Api.Template.Application.Dtos.Identity.Users;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Update;

namespace Minimal.Api.Template.Application.Commands.Identity.Users.Update;

/// <summary>
/// The update user password command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IUpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordDto, UserInfoDto>;