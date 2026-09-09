using Asp.Versioning.Builder;

namespace Minimal.Api.Template.Presentation.Api.Endpoints;

/// <summary>
/// The endpoint group.
/// </summary>
internal interface IEndpointGroup
{
    /// <summary>
    /// Maps the group and the endpoints of the group into the Web application.
    /// </summary>
    /// <param name="apiBuilder">The API endpoint builder.</param>
    static abstract void Map(IVersionedEndpointRouteBuilder apiBuilder);
}