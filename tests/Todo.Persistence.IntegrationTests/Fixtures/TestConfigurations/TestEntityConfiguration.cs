using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Persistence.Configurations;
using Todo.Persistence.IntegrationTests.Fixtures.TestEntities;

namespace Todo.Persistence.IntegrationTests.Fixtures.TestConfigurations;

internal class TestEntityConfiguration : BaseOwnedEntityConfiguration<TestEntity>
{
    public static string GetTableName() => "TestEntitiesTable";

    public override void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable(GetTableName());

        builder.Property(e => e.Code)
            .HasMaxLength(64);

        builder.HasIndex(e => e.Code).IsUnique();

        builder.OwnsOne(e => e.OwnedEntity, o =>
        {
            o.Property(e => e.Description).HasMaxLength(512);
        });
    }
}