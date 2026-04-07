using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

namespace Todo.Presentation.Api.OpenApi;

/// <summary>
/// The OpenAPI related extensions.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class OpenApiExtensions
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    /// <param name="services">The service collection.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds OpenAPI documents to the services.
        /// </summary>
        /// <param name="apiVersions">The API versions to add.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection AddOpenApiDocuments(IEnumerable<ApiVersion> apiVersions)
        {
            foreach (var version in apiVersions)
            {
                services.AddOpenApi(GetDocumentName(version), options =>
                {
                    options.AddDocumentTransformer<InfoDocumentTransformer>();
                    options.AddDocumentTransformer<SecuritySchemesDocumentTransformer>();
                    options.AddOperationTransformer<AuthorizationOperationTransformer>();
                    options.AddOperationTransformer<ApiVersionOperationTransformer>();
                    options.AddOperationTransformer<DeprecatedStatusOperationTransformer>();
                });
            }

            return services;
        }
    }

    /// <summary>
    /// The <see cref="WebApplication"/> extensions.
    /// </summary>
    /// <param name="app">The Web application.</param>
    extension(WebApplication app)
    {
        /// <summary>
        /// Register the Scalar UI into the application.
        /// </summary>
        /// <param name="apiVersions">The API versions to add.</param>
        /// <returns>The Web application.</returns>
        public WebApplication MapScalar(IEnumerable<ApiVersion> apiVersions)
        {
            var documentNames = apiVersions.Select(GetDocumentName).ToArray();

            app.MapScalarApiReference(options =>
            {
                options.WithTitle(ApiInfoDetails.Title);

                options.AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme)
                    .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme,
                        securityScheme =>
                        {
                            securityScheme.Token = "your-token";
                        });

                if (documentNames.Length != 0)
                {
                    options.AddDocuments(documentNames);
                }
            });

            return app;
        }
    }

    private static string GetDocumentName(ApiVersion apiVersion)
    {
        return $"v{apiVersion}";
    }
}