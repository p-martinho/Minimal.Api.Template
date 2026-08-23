using System.Diagnostics.CodeAnalysis;
using Minimal.Api.Template.Application.Dtos.Identity.Users;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Create;
using Minimal.Api.Template.Application.Dtos.Identity.Users.Update;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users.Create;
using Minimal.Api.Template.Presentation.Api.Dtos.V1.Identity.Users.Update;

namespace Minimal.Api.Template.Presentation.Api.MappingExtensions.V1.Identity.Users;

/// <summary>
/// The user mapping extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class UserMappingExtensions
{
    /// <summary>
    /// The <see cref="UserInfoDto"/> extensions.
    /// </summary>
    /// <param name="dto">The application DTO.</param>
    extension(UserInfoDto dto)
    {
        /// <summary>
        /// Converts the application DTO into an API DTO.
        /// </summary>
        /// <returns>The API DTO.</returns>
        public UserInfoApiDto ToApiDto()
        {
            return new UserInfoApiDto
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                FullName = dto.FullName
            };
        }
    }

    /// <summary>
    /// The <see cref="CreateUserApiDto"/> extensions.
    /// </summary>
    /// <param name="apiDto">The API DTO.</param>
    extension(CreateUserApiDto apiDto)
    {
        /// <summary>
        /// Converts the API DTO into an application DTO.
        /// </summary>
        /// <returns>The application DTO.</returns>
        public CreateUserDto ToDto()
        {
            return new CreateUserDto
            {
                Email = apiDto.Email,
                Password = apiDto.Password,
                FirstName = apiDto.FirstName,
                LastName = apiDto.LastName
            };
        }
    }

    /// <summary>
    /// The <see cref="UpdateUserInfoApiDto"/> extensions.
    /// </summary>
    /// <param name="apiDto">The API DTO.</param>
    extension(UpdateUserInfoApiDto apiDto)
    {
        /// <summary>
        /// Converts the API DTO into an application DTO.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The application DTO.</returns>
        public UpdateUserInfoDto ToDto(string userId)
        {
            return new UpdateUserInfoDto
            {
                Id = userId,
                Email = apiDto.Email,
                FirstName = apiDto.FirstName,
                LastName = apiDto.LastName
            };
        }
    }

    /// <summary>
    /// The <see cref="UpdateUserPasswordApiDto"/> extensions.
    /// </summary>
    /// <param name="apiDto">The API DTO.</param>
    extension(UpdateUserPasswordApiDto apiDto)
    {
        /// <summary>
        /// Converts the API DTO into an application DTO.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The application DTO.</returns>
        public UpdateUserPasswordDto ToDto(string userId)
        {
            return new UpdateUserPasswordDto
            {
                Id = userId,
                OldPassword = apiDto.OldPassword,
                NewPassword = apiDto.NewPassword
            };
        }
    }
}