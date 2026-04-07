using Microsoft.EntityFrameworkCore;
using Todo.Persistence.IntegrationTests.Fixtures.TestEntities;

namespace Todo.Persistence.IntegrationTests.Fixtures.TestServices;

internal class TestDbContext : ApplicationDbContext
{
    public DbSet<TestEntity> TestEntities { get; set; }

    public DbSet<TestChildEntity> TestChildEntities { get; set; }

    protected override string DefaultSchema => GetDefaultSchema();

    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public static string GetDefaultSchema() => "testing";
}