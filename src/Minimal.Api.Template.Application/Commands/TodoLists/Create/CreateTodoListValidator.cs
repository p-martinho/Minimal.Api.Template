using FluentValidation;
using Minimal.Api.Template.Application.Dtos.TodoLists.Create;

namespace Minimal.Api.Template.Application.Commands.TodoLists.Create;

/// <summary>
/// The validator for <see cref="CreateTodoListDto"/>.
/// </summary>
/// <seealso cref="AbstractValidator{T}"/>
internal class CreateTodoListValidator : AbstractValidator<CreateTodoListDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTodoListValidator"/> class.
    /// </summary>
    public CreateTodoListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);
    }
}