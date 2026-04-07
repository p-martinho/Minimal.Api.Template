using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
using Todo.Persistence.Constants;

namespace Todo.Persistence.Configurations;

/// <summary>
/// The base owned entity configuration.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="BaseAuditableEntityConfiguration{TEntity}"/>
[ExcludeFromCodeCoverage]
internal abstract class BaseOwnedEntityConfiguration<TEntity> : BaseAuditableEntityConfiguration<TEntity>
    where TEntity : BaseOwnedEntity
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.OwnerId).HasMaxLength(MaxLength.Guid);
    }

    /// <inheritdoc />
    protected override Type? GetDirectlyDerivedType()
    {
        return null;
    }
}