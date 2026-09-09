using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal.Api.Template.Persistence.Configurations;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestConfigurations;

internal class TestChildEntityConfiguration : BaseAuditableEntityConfiguration<TestChildEntity>
{
    public static string GetTableName() => "TestChildEntitiesTable";

    public override void Configure(EntityTypeBuilder<TestChildEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable(GetTableName());

        builder.Property(e => e.Code)
            .HasMaxLength(64);

        builder.HasIndex(e => e.Code).IsUnique();
    }
}