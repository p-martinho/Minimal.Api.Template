using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using Todo.Application.Commands.Models;

namespace Todo.Application.Commands.Identity.OpenId.Seed;

/// <summary>
/// The seed OpenId testing resources command handler.
/// </summary>
/// <seealso cref="CommandHandler{TIn,TOutData}"/>
/// <seealso cref="ISeedOpenIdTestingResourcesCommandHandler"/>
[ExcludeFromCodeCoverage]
internal class SeedOpenIdTestingResourcesCommandHandler : CommandHandler<bool>,
    ISeedOpenIdTestingResourcesCommandHandler
{
    private const string TodoScopeName = "todo_app";
    private const string TodoApiResourceName = "todo_api";

    private readonly IHostEnvironment _hostEnvironment;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly IOpenIddictApplicationManager _applicationManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SeedOpenIdTestingResourcesCommandHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="hostEnvironment">The host environment.</param>
    /// <param name="scopeManager">The OpenId scope manager.</param>
    /// <param name="applicationManager">The OpenId application manager.</param>
    public SeedOpenIdTestingResourcesCommandHandler(ILogger<SeedOpenIdTestingResourcesCommandHandler> logger,
        IHostEnvironment hostEnvironment,
        IOpenIddictScopeManager scopeManager,
        IOpenIddictApplicationManager applicationManager)
        : base(logger)
    {
        _hostEnvironment = hostEnvironment;
        _scopeManager = scopeManager;
        _applicationManager = applicationManager;
    }

    /// <inheritdoc />
    protected override async Task<CommandOut<bool>> ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_hostEnvironment.IsDevelopment())
        {
            return CommandOut<bool>.ValidationError("Environment is not development.");
        }

        await PopulateScopesAsync(cancellationToken);

        await PopulateApplicationsAsync(cancellationToken);

        return CommandOut<bool>.Success(true);
    }

    private async ValueTask PopulateScopesAsync(CancellationToken cancellationToken)
    {
        var scopeDescriptors = GetScopes();

        foreach (var scopeDescriptor in scopeDescriptors)
        {
            await CreateOrUpdateScopeAsync(scopeDescriptor, cancellationToken);
        }
    }

    private static OpenIddictScopeDescriptor[] GetScopes()
    {
        return
        [
            new OpenIddictScopeDescriptor { Name = TodoScopeName, Resources = { TodoApiResourceName } }
        ];
    }

    private async Task CreateOrUpdateScopeAsync(OpenIddictScopeDescriptor scopeDescriptor,
        CancellationToken cancellationToken)
    {
        var scopeInstance = await _scopeManager.FindByNameAsync(scopeDescriptor.Name!, cancellationToken);

        if (scopeInstance is null)
        {
            await _scopeManager.CreateAsync(scopeDescriptor, cancellationToken);

            Logger.LogInformation("Created scope {ScopeName}", scopeDescriptor.Name);
        }
        else
        {
            await _scopeManager.UpdateAsync(scopeInstance, scopeDescriptor, cancellationToken);

            Logger.LogInformation("Updated scope {ScopeName}", scopeDescriptor.Name);
        }
    }

    private async ValueTask PopulateApplicationsAsync(CancellationToken cancellationToken)
    {
        var appDescriptors = GetApplications();

        foreach (var appDescriptor in appDescriptors)
        {
            await CreateOrUpdateApplicationAsync(appDescriptor, cancellationToken);
        }
    }

    private static OpenIddictApplicationDescriptor[] GetApplications()
    {
        return
        [
            new OpenIddictApplicationDescriptor
            {
                ClientId = "test_client",
                ClientSecret = "test_secret",
                ClientType = OpenIddictConstants.ClientTypes.Confidential,
                DisplayName = "Client Test",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                    OpenIddictConstants.Permissions.Prefixes.Scope + TodoScopeName
                }
            }
        ];
    }

    private async Task CreateOrUpdateApplicationAsync(OpenIddictApplicationDescriptor appDescriptor,
        CancellationToken cancellationToken)
    {
        var client = await _applicationManager.FindByClientIdAsync(appDescriptor.ClientId!, cancellationToken);

        if (client is null)
        {
            await _applicationManager.CreateAsync(appDescriptor, cancellationToken);

            Logger.LogInformation("Created application {ClientId}", appDescriptor.ClientId);
        }
        else
        {
            await _applicationManager.UpdateAsync(client, appDescriptor, cancellationToken);

            Logger.LogInformation("Updated application {ClientId}", appDescriptor.ClientId);
        }
    }
}