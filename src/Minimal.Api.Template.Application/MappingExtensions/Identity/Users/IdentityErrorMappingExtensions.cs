using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Minimal.Api.Template.Application.Common.Models;

namespace Minimal.Api.Template.Application.MappingExtensions.Identity.Users;

/// <summary>
/// The identity error extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class IdentityErrorMappingExtensions
{
    /// <summary>
    /// The <see cref="IdentityError"/> extensions.
    /// </summary>
    /// <param name="error">The identity error.</param>
    extension(IdentityError error)
    {
        /// <summary>
        /// Converts the identity error into a result detail.
        /// </summary>
        /// <returns>The result detail.</returns>
        public ResultDetail ToResultDetail()
        {
            return new ResultDetail(error.Code, error.Description);
        }
    }
}