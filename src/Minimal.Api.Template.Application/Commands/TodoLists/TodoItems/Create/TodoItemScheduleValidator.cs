using FluentValidation;
using Minimal.Api.Template.Application.Dtos.TodoLists.TodoItems;

namespace Minimal.Api.Template.Application.Commands.TodoLists.TodoItems.Create;

/// <summary>
/// The validator for <see cref="TodoItemScheduleDto"/>.
/// </summary>
/// <seealso cref="AbstractValidator{T}"/>
internal class TodoItemScheduleValidator : AbstractValidator<TodoItemScheduleDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoItemScheduleValidator"/> class.
    /// </summary>
    public TodoItemScheduleValidator()
    {
        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTimeOffset.UtcNow)
            .When(x => x.DueDate.HasValue);
    }
}