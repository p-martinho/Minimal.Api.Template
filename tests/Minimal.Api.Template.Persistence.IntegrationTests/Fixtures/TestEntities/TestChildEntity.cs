using Minimal.Api.Template.Domain.Abstractions;
using Minimal.Api.Template.Domain.Entities;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;

public class TestChildEntity : BaseAuditableEntity, IAggregateEntity, ISoftDeletableEntity
{
    public string Code { get; set; } = null!;

    public TestEntity Parent { get; set; } = null!;
}