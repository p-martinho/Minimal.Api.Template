using FluentValidation;
using Microsoft.Extensions.Logging;
using Minimal.Api.Template.Application.Commands.Models;
using Minimal.Api.Template.Application.Dtos.TodoLists;
using Minimal.Api.Template.Application.Dtos.TodoLists.Update;
using Minimal.Api.Template.Application.MappingExtensions.TodoLists;
using Minimal.Api.Template.Domain.Entities.TodoLists;
using Minimal.Api.Template.Persistence.Repositories.TodoLists;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Update;

/// <summary>
/// The update to do list command handler.
/// </summary>
/// <seealso cref="CommandHandler{TIn,TOutData}"/>
/// <seealso cref="IUpdateTodoListCommandHandler"/>
internal class UpdateTodoListCommandHandler : CommandHandler<UpdateTodoListDto, TodoListDto>,
    IUpdateTodoListCommandHandler
{
    private readonly ITodoListRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTodoListCommandHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="validator">The validator.</param>
    /// <param name="repository">The to do list repository.</param>
    public UpdateTodoListCommandHandler(ILogger<UpdateTodoListCommandHandler> logger,
        IValidator<UpdateTodoListDto> validator,
        ITodoListRepository repository)
        : base(logger, validator)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    protected override async Task<CommandOut<TodoListDto>> HandleCommandInAsync(UpdateTodoListDto commandIn,
        CancellationToken cancellationToken)
    {
        if (commandIn.Id == Guid.Empty)
        {
            return CommandOut<TodoListDto>.NotFoundError();
        }

        var todoList = await _repository.GetByIdAsync(commandIn.Id, cancellationToken: cancellationToken);

        if (todoList is null)
        {
            return CommandOut<TodoListDto>.NotFoundError();
        }

        UpdateTodoList(commandIn, todoList);

        await _repository.SaveChangesAsync(cancellationToken);

        return CommandOut<TodoListDto>.Success(todoList.ToDto());
    }

    private static void UpdateTodoList(UpdateTodoListDto commandIn, TodoList todoList)
    {
        if (!string.IsNullOrWhiteSpace(commandIn.Name))
        {
            todoList.UpdateName(commandIn.Name);
        }
    }
}