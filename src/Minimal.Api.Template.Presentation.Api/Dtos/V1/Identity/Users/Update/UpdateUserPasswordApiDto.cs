using System.Diagnostics.CodeAnalysis;

namespace Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users.Update;

/// <summary>
/// The update user password API DTO.
/// </summary>
[ExcludeFromCodeCoverage]
public record UpdateUserPasswordApiDto
{
    /// <summary>
    /// The user's old password.
    /// </summary>
    public required string OldPassword { get; init; }

    /// <summary>
    /// The user's new password.
    /// </summary>
    public required string NewPassword { get; init; }
}