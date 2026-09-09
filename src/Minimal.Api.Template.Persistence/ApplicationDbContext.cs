using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Minimal.Api.Template.Persistence.Extensions;

namespace Minimal.Api.Template.Persistence;

/// <summary>
/// The application DB context.
/// </summary>
/// <seealso cref="DbContext"/>
[ExcludeFromCodeCoverage]
internal class ApplicationDbContext : DbContext
{
    /// <summary>
    /// The default schema to be used, when the schema is not set on the entity configuration.
    /// </summary>
    protected virtual string DefaultSchema => "todo";

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!string.IsNullOrWhiteSpace(DefaultSchema))
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.AddSoftDeleteProperty();
    }
}