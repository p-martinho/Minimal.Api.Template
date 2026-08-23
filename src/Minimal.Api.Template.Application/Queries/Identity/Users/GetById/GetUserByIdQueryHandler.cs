using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Minimal.Api.Template.Application.Dtos.Identity.Users;
using Minimal.Api.Template.Application.MappingExtensions.Identity.Users;
using Minimal.Api.Template.Application.Queries.Models;
using Minimal.Api.Template.Common.ApplicationContext;
using Minimal.Api.Template.Common.Authorization;
using Minimal.Api.Template.Domain.Entities.Identity.Users;

namespace Minimal.Api.Template.Application.Queries.Identity.Users.GetById;

/// <summary>
/// The get user by id query handler.
/// </summary>
/// <seealso cref="IGetUserByIdQueryHandler"/>
internal class GetUserByIdQueryHandler : QueryHandler<string, UserInfoDto>, IGetUserByIdQueryHandler
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUser">The current user.</param>
    /// <param name="userManager">The user manager.</param>
    public GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger,
        ICurrentUser currentUser,
        UserManager<AppIdentityUser> userManager)
        : base(logger)
    {
        _currentUser = currentUser;
        _userManager = userManager;
    }

    /// <inheritdoc />
    protected override async Task<QueryOut<UserInfoDto>> HandleQueryInAsync(string queryIn,
        CancellationToken cancellationToken)
    {
        if (!HasPermissionsForOperation(queryIn))
        {
            return QueryOut<UserInfoDto>.NotFoundError();
        }

        var user = await _userManager.FindByIdAsync(queryIn);

        return user is null
            ? QueryOut<UserInfoDto>.NotFoundError()
            : QueryOut<UserInfoDto>.Success(user.ToDto());
    }

    private bool HasPermissionsForOperation(string userId)
    {
        return _currentUser.UserId == userId || _currentUser.IsInRole(UserRoles.Admin);
    }
}