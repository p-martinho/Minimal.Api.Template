using Microsoft.Extensions.Options;
using Todo.Persistence.IntegrationTests.Fixtures.TestEntities;
using Todo.Persistence.IntegrationTests.Fixtures.TestServices;
using Todo.Persistence.Repositories;
using Todo.Persistence.Repositories.Settings;

namespace Todo.Persistence.IntegrationTests.Fixtures.TestRepositories;

internal class TestRepository : Repository<TestEntity>
{
    public TestRepository(TestDbContext context,
        IOptionsSnapshot<QueryParametersSettings> queryParametersOptions)
        : base(context, queryParametersOptions)
    {
    }
}