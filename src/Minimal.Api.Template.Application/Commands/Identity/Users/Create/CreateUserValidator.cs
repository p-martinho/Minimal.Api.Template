using FluentValidation;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Create;

namespace Minimal.Api.Template.Application.Commands.Identity.Users.Create;

/// <summary>
/// The validator for <see cref="CreateUserDto"/>.
/// </summary>
/// <seealso cref="AbstractValidator{T}"/>
internal class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
    /// </summary>
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .MaximumLength(64);

        RuleFor(x => x.LastName)
            .MaximumLength(64);
    }
}