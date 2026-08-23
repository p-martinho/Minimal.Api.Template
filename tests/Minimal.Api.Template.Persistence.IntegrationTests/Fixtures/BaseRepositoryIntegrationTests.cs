using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Template.Persistence;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures;

public abstract class BaseRepositoryIntegrationTests : IClassFixture<EfCoreFixture>, IDisposable
{
    private readonly IServiceScope _testScope;
    
    internal readonly ApplicationDbContext DbContext;

    protected BaseRepositoryIntegrationTests(EfCoreFixture fixture)
    {
        _testScope = fixture.ServiceProvider.CreateScope();

        DbContext = _testScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _testScope.Dispose();
    }
}