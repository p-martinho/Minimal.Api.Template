using Microsoft.Extensions.Options;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestServices;
using Minimal.Api.Template.Persistence.Repositories;
using Minimal.Api.Template.Persistence.Repositories.Settings;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestRepositories;

internal class TestRepository : Repository<TestEntity>
{
    public TestRepository(TestDbContext context,
        IOptionsSnapshot<QueryParametersSettings> queryParametersOptions)
        : base(context, queryParametersOptions)
    {
    }
}