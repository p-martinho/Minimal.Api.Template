using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;
using Todo.Common.ApplicationContext;
using Todo.Persistence.Constants;
using Todo.Persistence.DependencyInjection;
using Todo.Persistence.IntegrationTests.Fixtures;
using Todo.Persistence.IntegrationTests.Fixtures.TestServices;

[assembly: AssemblyFixture(typeof(EfCoreFixture))]

namespace Todo.Persistence.IntegrationTests.Fixtures;

public class EfCoreFixture : IAsyncLifetime
{
    private const string MsSqlImageName = "mcr.microsoft.com/mssql/server:2022-CU24-ubuntu-22.04";

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder(MsSqlImageName).Build();

    public IServiceProvider ServiceProvider = null!;

    public virtual async ValueTask InitializeAsync()
    {
        await _msSqlContainer.StartAsync(TestContext.Current.CancellationToken);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { $"ConnectionStrings:{ConnectionStrings.SqlDefault}", _msSqlContainer.GetConnectionString() }
            })
            .Build();

        var services = new ServiceCollection();

        // IHostEnvironment is used to decide whether it should apply migrations.
        services.AddScoped<IHostEnvironment, TestHostEnvironment>();

        // One of the EF Core interceptors uses ICurrentUser to set audit properties.
        services.AddScoped<ICurrentUser, TestCurrentUser>();

        // Adding using DI, for integration testing, to include interceptors, migration, etc.
        services.AddEfCore<TestDbContext>(configuration);
        services.AddEfCore<ApplicationDbContext>(configuration);

        ServiceProvider = services.BuildServiceProvider();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        GC.SuppressFinalize(this);
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        return _msSqlContainer.DisposeAsync();
    }
}