using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using Todo.Application.Commands.Identity.Users.Create;
using Todo.Application.Commands.Identity.Users.Update;
using Todo.Application.Commands.Models;
using Todo.Application.Dtos.Identity.Users;
using Todo.Application.Queries.Identity.Users.GetById;
using Todo.Application.Queries.Models;
using Todo.Common.ApplicationContext;
using Todo.Presentation.Api.Dtos.V1.Identity.Users.Create;
using Todo.Presentation.Api.Dtos.V1.Identity.Users.Update;
using Todo.Presentation.Api.Endpoints.V1.Identity;

namespace Todo.Presentation.Api.IntegrationTests.Endpoints.V1.Identity;

/// <remarks>
/// These tests are complementary to the integration tests, to test scenarios not possible to test in the integration tests (for instance, internal errors).
/// </remarks>
public class UsersEndpointGroupTests
{
    [Fact]
    public async Task CreateUserAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<ICreateUserCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<UserInfoDto>.InternalError());
        var request = new CreateUserApiDto { Email = "Email", Password = "Password" };

        // Act
        var result = await UsersEndpointGroup.CreateUserAsync(request,
            commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task GetUserInfoAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns("userId");
        var queryHandler = Substitute.For<IGetUserByIdQueryHandler>();
        queryHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(QueryOut<UserInfoDto>.InternalError());

        // Act
        var result =
            await UsersEndpointGroup.GetUserInfoAsync(currentUser, queryHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task GetUserInfoAsync_WhenUserIdIsNull_ShouldReturnUnauthorized()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns((string?)null);
        var queryHandler = Substitute.For<IGetUserByIdQueryHandler>();

        // Act
        var result =
            await UsersEndpointGroup.GetUserInfoAsync(currentUser, queryHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as UnauthorizedHttpResult;
        Assert.NotNull(problemHttpResult);
    }

    [Fact]
    public async Task UpdateUserInfoAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns("userId");
        var commandHandler = Substitute.For<IUpdateUserInfoCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<UserInfoDto>.InternalError());
        var request = new UpdateUserInfoApiDto();

        // Act
        var result = await UsersEndpointGroup.UpdateUserInfoAsync(request, currentUser, commandHandler,
            TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task UpdateUserInfoAsync_WhenUserIdIsNull_ShouldReturnUnauthorized()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns((string?)null);
        var commandHandler = Substitute.For<IUpdateUserInfoCommandHandler>();
        var request = new UpdateUserInfoApiDto();

        // Act
        var result = await UsersEndpointGroup.UpdateUserInfoAsync(request, currentUser, commandHandler,
            TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as UnauthorizedHttpResult;
        Assert.NotNull(problemHttpResult);
    }

    [Fact]
    public async Task UpdateUserPasswordAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns("userId");
        var commandHandler = Substitute.For<IUpdateUserPasswordCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<UserInfoDto>.InternalError());
        var request = new UpdateUserPasswordApiDto { OldPassword = "OldPassword", NewPassword = "NewPassword" };

        // Act
        var result = await UsersEndpointGroup.UpdateUserPasswordAsync(request, currentUser, commandHandler,
            TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task UpdateUserPasswordAsync_WhenUserIdIsNull_ShouldReturnUnauthorized()
    {
        // Arrange
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.UserId.Returns((string?)null);
        var commandHandler = Substitute.For<IUpdateUserPasswordCommandHandler>();
        var request = new UpdateUserPasswordApiDto { OldPassword = "OldPassword", NewPassword = "NewPassword" };

        // Act
        var result = await UsersEndpointGroup.UpdateUserPasswordAsync(request, currentUser, commandHandler,
            TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as UnauthorizedHttpResult;
        Assert.NotNull(problemHttpResult);
    }
}