using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Minimal.Api.Template.Common.ApplicationContext;
using Minimal.Api.Template.Common.Authorization;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestServices;
using Minimal.Api.Template.Persistence.Repositories;
using Minimal.Api.Template.Persistence.Repositories.Settings;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestRepositories;

internal class TestQueryRepository : QueryRepository<TestEntity>
{
    private readonly ICurrentUser _currentUser;

    public TestQueryRepository(TestDbContext context,
        IOptionsSnapshot<QueryParametersSettings> queryParametersOptions,
        ICurrentUser currentUser)
        : base(context, queryParametersOptions)
    {
        _currentUser = currentUser;
    }

    protected override Func<IQueryable<TestEntity>, IIncludableQueryable<TestEntity, object>>
        GetDefaultAggregateIncludes()
    {
        return q => q.Include(e => e.Children);
    }

    protected override Expression<Func<TestEntity, bool>> GetDefaultPermissionsFilter()
    {
        return e => _currentUser.IsInRole(UserRoles.Admin) ||
                    e.OwnerId == _currentUser.UserId;
    }
}