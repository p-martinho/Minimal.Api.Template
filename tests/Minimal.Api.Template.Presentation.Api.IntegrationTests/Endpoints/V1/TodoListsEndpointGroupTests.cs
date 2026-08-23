using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Minimal.Api.Template.Application.Commands.Models;
using Minimal.Api.Template.Application.Commands.TodoLists.Create;
using Minimal.Api.Template.Application.Commands.TodoLists.Delete;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Create;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Delete;
using Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Update;
using Minimal.Api.Template.Application.Commands.TodoLists.Update;
using Minimal.Api.Template.Application.Dtos;
using Minimal.Api.Template.Application.Dtos.TodoLists;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems;
using Minimal.Api.Template.Application.Queries.Models;
using Minimal.Api.Template.Application.Queries.TodoLists.Get;
using Minimal.Api.Template.Application.Queries.TodoLists.GetById;
using Minimal.Api.Template.Presentation.Api.Dtos;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.TodoLists.Create;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.TodoLists.TodoItems.Create;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.TodoLists.TodoItems.Update;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.TodoLists.Update;
using Minimal.Api.Template.Presentation.Api.Endpoints.V1;
using NSubstitute;

namespace Minimal.Api.Template.Presentation.Api.IntegrationTests.Endpoints.V1;

/// <remarks>
/// These tests are complementary to the integration tests, to test scenarios not possible to test in the integration tests (for instance, internal errors).
/// </remarks>
public class TodoListsEndpointGroupTests
{
    [Fact]
    public async Task CreateTodoListAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<ICreateTodoListCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoListDto>.InternalError());
        var request = new CreateTodoListApiDto { Name = "Name" };

        // Act
        var result = await TodoListsEndpointGroup.CreateTodoListAsync(request,
            commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task GetTodoListByIdAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var queryHandler = Substitute.For<IGetTodoListByIdQueryHandler>();
        queryHandler.HandleAsync(Guid.Empty, CancellationToken.None)
            .ReturnsForAnyArgs(QueryOut<TodoListDto>.InternalError());

        // Act
        var result = await TodoListsEndpointGroup.GetTodoListByIdAsync(Guid.Empty,
            queryHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task GetTodoListsAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var queryHandler = Substitute.For<IGetTodoListsQueryHandler>();
        queryHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(QueryOut<PaginatedQueryResultDto<TodoListDto>>.InternalError());
        var request = new PaginatedQueryApiDto();

        // Act
        var result = await TodoListsEndpointGroup.GetTodoListsAsync(request, queryHandler,
                TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task UpdateTodoListAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<IUpdateTodoListCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoListDto>.InternalError());
        var request = new UpdateTodoListApiDto();

        // Act
        var result = await TodoListsEndpointGroup.UpdateTodoListAsync(Guid.Empty,
            request, commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task DeleteTodoListAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<IDeleteTodoListCommandHandler>();
        commandHandler.HandleAsync(Guid.Empty, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoListDto>.InternalError());

        // Act
        var result = await TodoListsEndpointGroup.DeleteTodoListAsync(Guid.Empty,
            commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task CreateTodoItemAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<ICreateTodoItemCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoItemDto>.InternalError());
        var request = new CreateTodoItemApiDto { Title = "Title" };

        // Act
        var result = await TodoListsEndpointGroup.CreateTodoItemAsync(Guid.Empty,
            request, commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task UpdateTodoItemAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<IUpdateTodoItemCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoItemDto>.InternalError());
        var request = new UpdateTodoItemApiDto();

        // Act
        var result = await TodoListsEndpointGroup.UpdateTodoItemAsync(Guid.Empty,
            Guid.Empty, request, commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }

    [Fact]
    public async Task DeleteTodoItemAsync_WhenInternalError_ShouldReturnProblemDetails()
    {
        // Arrange
        var commandHandler = Substitute.For<IDeleteTodoItemCommandHandler>();
        commandHandler.HandleAsync(null!, CancellationToken.None)
            .ReturnsForAnyArgs(CommandOut<TodoItemDto>.InternalError());

        // Act
        var result = await TodoListsEndpointGroup.DeleteTodoItemAsync(Guid.Empty,
            Guid.Empty, commandHandler, TestContext.Current.CancellationToken);

        // Assert
        var problemHttpResult = result.Result as ProblemHttpResult;
        Assert.NotNull(problemHttpResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.StatusCode);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemHttpResult.ProblemDetails.Status);
    }
}