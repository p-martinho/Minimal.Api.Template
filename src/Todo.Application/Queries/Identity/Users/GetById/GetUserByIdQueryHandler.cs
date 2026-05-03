using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Application.Dtos.Identity.Users;
using Todo.Application.MappingExtensions.Identity.Users;
using Todo.Application.Queries.Models;
using Todo.Common.ApplicationContext;
using Todo.Common.Authorization;
using Todo.Domain.Entities.Identity.Users;

namespace Todo.Application.Queries.Identity.Users.GetById;

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