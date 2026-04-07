using Asp.Versioning.Builder;

namespace Todo.Presentation.Api.Endpoints;

/// <summary>
/// The endpoint group.
/// </summary>
internal interface IEndpointGroup
{
    /// <summary>
    /// Maps the group and the endpoints of the group into the Web application.
    /// </summary>
    /// <param name="app">The Web application.</param>
    /// <param name="apiVersionSet">The API version set.</param>
    static abstract void Map(WebApplication app, ApiVersionSet apiVersionSet);
}