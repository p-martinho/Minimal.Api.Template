using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Template.Domain.Entities.Identity.Users;

namespace Minimal.Api.Template.Presentation.Api.IntegrationTests.Fixtures.Identity;

public class IdentityBaseIntegrationTests : IAsyncDisposable
{
    private readonly IServiceScope _testScope;

    protected readonly HttpClient Client;
    protected readonly UserManager<AppIdentityUser> UserManager;

    protected IdentityBaseIntegrationTests(IdentityIntegrationTestWebAppFactory factory)
    {
        _testScope = factory.Services.CreateScope();

        Client = factory.CreateClient();
        Client.BaseAddress = new Uri("https://localhost"); // Set to HTTPS

        UserManager = _testScope.ServiceProvider.GetRequiredService<UserManager<AppIdentityUser>>();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        GC.SuppressFinalize(this);
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        UserManager.Dispose();
        _testScope.Dispose();

        return default;
    }
}