using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities.Identity.Users;
using Todo.Domain.ValueObjects.Identity.Users;

namespace Todo.Persistence;

/// <summary>
/// The identity DB context.
/// </summary>
/// <seealso cref="IdentityDbContext{TUser}"/>
[ExcludeFromCodeCoverage]
internal class IdentityDbContext : IdentityDbContext<IdentityUser>
{
    private const string DefaultSchema = "identity";

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseOpenIddict();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!string.IsNullOrWhiteSpace(DefaultSchema))
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
        }

        ApplyUserEntityConfiguration(modelBuilder.Entity<AppIdentityUser>());
    }

    private static void ApplyUserEntityConfiguration(EntityTypeBuilder<AppIdentityUser> builder)
    {
        builder.OwnsOne(e => e.Name, s =>
        {
            s.Property(e => e.FirstName)
                .HasColumnName(nameof(AppIdentityUserName.FirstName))
                .HasMaxLength(64);

            s.Property(e => e.LastName)
                .HasColumnName(nameof(AppIdentityUserName.LastName))
                .HasMaxLength(64);
        });
    }
}