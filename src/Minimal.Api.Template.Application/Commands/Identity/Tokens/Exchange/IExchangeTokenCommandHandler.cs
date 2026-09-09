using System.Security.Claims;
using OpenIddict.Abstractions;

namespace Minimal.Api.Template.Application.Commands.Identity.Tokens.Exchange;

/// <summary>
/// The exchange token command handler.
/// </summary>
/// <seealso cref="ICommandHandler{TIn,TOutData}"/>
public interface IExchangeTokenCommandHandler : ICommandHandler<OpenIddictRequest, ClaimsPrincipal>;