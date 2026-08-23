using FluentValidation;
using Minimal.Api.Template.Application.Dtos.TodoLists.Update;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Update;

/// <summary>
/// The validator for <see cref="UpdateTodoListDto"/>.
/// </summary>
/// <seealso cref="AbstractValidator{T}"/>
internal class UpdateTodoListValidator : AbstractValidator<UpdateTodoListDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTodoListValidator"/> class.
    /// </summary>
    public UpdateTodoListValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64)
            .When(x => x.Name is not null);
    }
}