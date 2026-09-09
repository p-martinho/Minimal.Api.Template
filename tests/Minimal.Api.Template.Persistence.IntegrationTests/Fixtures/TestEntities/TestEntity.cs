using Minimal.Api.Template.Domain.Abstractions;
using Minimal.Api.Template.Domain.Entities;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;

public class TestEntity : BaseOwnedEntity, IAggregateEntity, ISoftDeletableEntity
{
    public string Code { get; set; } = null!;

    public IList<TestChildEntity> Children { get; set; } = null!;

    public TestOwnedEntity OwnedEntity { get; set; } = new();
}

public class TestOwnedEntity
{
    public string Description { get; set; } = string.Empty;
}