using Microsoft.EntityFrameworkCore;
using Minimal.Api.Template.Persistence;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestServices;

internal class TestDbContext : ApplicationDbContext
{
    public DbSet<TestEntity> TestEntities { get; set; }

    public DbSet<TestChildEntity> TestChildEntities { get; set; }

    protected override string DefaultSchema => GetDefaultSchema();

    public TestDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public static string GetDefaultSchema() => "testing";
}