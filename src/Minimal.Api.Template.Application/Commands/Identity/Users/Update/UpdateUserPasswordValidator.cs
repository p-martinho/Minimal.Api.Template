using FluentValidation;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Update;

namespace Minimal.Api.Template.Application.Commands.Identity.Users.Update;

/// <summary>
/// The validator for <see cref="UpdateUserPasswordDto"/>.
/// </summary>
/// <seealso cref="AbstractValidator{T}"/>
internal class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserPasswordValidator"/> class.
    /// </summary>
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}