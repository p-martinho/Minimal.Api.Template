using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Template.Common.ApplicationContext;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestServices;

[assembly: AssemblyFixture(typeof(TestDataEfCoreFixture))]

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures;

public sealed class TestDataEfCoreFixture : EfCoreFixture
{
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();

        using var scope = ServiceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();

        var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();

        var databaseSeeder = new DatabaseSeeder(context, currentUser);

        await databaseSeeder.SeedDatabaseAsync(TestContext.Current.CancellationToken);
    }
}