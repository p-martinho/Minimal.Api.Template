using Todo.Domain.Abstractions;
using Todo.Domain.Entities;

namespace Todo.Persistence.IntegrationTests.Fixtures.TestEntities;

public class TestChildEntity : BaseAuditableEntity, IAggregateEntity, ISoftDeletableEntity
{
    public string Code { get; set; } = null!;

    public TestEntity Parent { get; set; } = null!;
}