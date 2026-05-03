using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Todo.Persistence.IntegrationTests.Fixtures.TestServices;

namespace Todo.Persistence.IntegrationTests.Fixtures;

/// <summary>
/// The test DB context factory.
/// </summary>
/// <remarks>This class is only needed to create migrations using the command (in the project folder): <code>dotnet ef migrations add InitialMigration</code></remarks>
internal class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
{
    public TestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer();

        return new TestDbContext(optionsBuilder.Options);
    }
}