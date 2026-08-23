using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Minimal.Api.Template.Application.Commands.Identity.Users.Create;
using Minimal.Api.Template.Application.Commands.Identity.Users.Update;
using Minimal.Api.Template.Application.Queries.Identity.Users.GetById;
using Minimal.Api.Template.Common.ApplicationContext;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users.Create;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users.Update;
using Minimal.Api.Template.Presentation.Api.Extensions;
using Minimal.Api.Template.Presentation.Api.MappingExtensions;
using Minimal.Api.Template.Presentation.Api.MappingExtensions.V1.Identity.Users;

namespace Minimal.Api.Template.Presentation.Api.Endpoints.V1.Identity;

/// <summary>
/// The users endpoint group.
/// </summary>
/// <seealso cref="IEndpointGroup"/>
internal sealed class UsersEndpointGroup : IEndpointGroup
{
    private const string EndpointGroupName = "Users";

    private static readonly ApiVersion ApiVersion = new(1, 0);

    /// <inheritdoc />
    public static void Map(IVersionedEndpointRouteBuilder apiBuilder)
    {
        var group = apiBuilder.MapEndpointGroup(EndpointGroupName, ApiVersion, isAuthorizationRequired: true);

        group.MapPost("", CreateUserAsync)
            .AllowAnonymous()
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("info", GetUserInfoAsync);

        group.MapPatch("info", UpdateUserInfoAsync)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("password", UpdateUserPasswordAsync)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    /// <summary>
    /// Creates the user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="commandHandler">The command handler.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response result.</returns>
    public static async Task<Results<Created<UserInfoApiDto>, ProblemHttpResult>>
        CreateUserAsync(CreateUserApiDto request, ICreateUserCommandHandler commandHandler,
            CancellationToken cancellationToken)
    {
        var commandOut = await commandHandler.HandleAsync(request.ToDto(), cancellationToken);

        if (!commandOut.Result.IsSuccess)
        {
            return TypedResults.Problem(commandOut.Result.ToProblemDetails());
        }

        return TypedResults.Created($"api/{EndpointGroupName}/{commandOut.Data?.Id}", commandOut.Data?.ToApiDto());
    }

    /// <summary>
    /// Gets the logged user info.
    /// </summary>
    /// <param name="currentUser">The current user.</param>
    /// <param name="queryHandler">The query handler.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response result.</returns>
    public static async Task<Results<Ok<UserInfoApiDto>, UnauthorizedHttpResult, ProblemHttpResult>>
        GetUserInfoAsync(ICurrentUser currentUser, IGetUserByIdQueryHandler queryHandler,
            CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;

        if (userId is null)
        {
            return TypedResults.Unauthorized();
        }

        var queryOut = await queryHandler.HandleAsync(userId, cancellationToken);

        if (!queryOut.Result.IsSuccess)
        {
            return TypedResults.Problem(queryOut.Result.ToProblemDetails());
        }

        return TypedResults.Ok(queryOut.Data?.ToApiDto());
    }

    /// <summary>
    /// Updates the logged user info.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="currentUser">The current user.</param>
    /// <param name="commandHandler">The command handler.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response result.</returns>
    public static async Task<Results<Ok<UserInfoApiDto>, UnauthorizedHttpResult, ProblemHttpResult>>
        UpdateUserInfoAsync(UpdateUserInfoApiDto request, ICurrentUser currentUser,
            IUpdateUserInfoCommandHandler commandHandler, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;

        if (userId is null)
        {
            return TypedResults.Unauthorized();
        }

        var commandOut = await commandHandler.HandleAsync(request.ToDto(userId), cancellationToken);

        if (!commandOut.Result.IsSuccess)
        {
            return TypedResults.Problem(commandOut.Result.ToProblemDetails());
        }

        return TypedResults.Ok(commandOut.Data?.ToApiDto());
    }

    /// <summary>
    /// Updates the logged user password.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="currentUser">The current user.</param>
    /// <param name="commandHandler">The command handler.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response result.</returns>
    public static async Task<Results<Ok, UnauthorizedHttpResult, ProblemHttpResult>>
        UpdateUserPasswordAsync(UpdateUserPasswordApiDto request, ICurrentUser currentUser,
            IUpdateUserPasswordCommandHandler commandHandler, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;

        if (userId is null)
        {
            return TypedResults.Unauthorized();
        }

        var commandOut = await commandHandler.HandleAsync(request.ToDto(userId), cancellationToken);

        if (!commandOut.Result.IsSuccess)
        {
            return TypedResults.Problem(commandOut.Result.ToProblemDetails());
        }

        return TypedResults.Ok();
    }
}