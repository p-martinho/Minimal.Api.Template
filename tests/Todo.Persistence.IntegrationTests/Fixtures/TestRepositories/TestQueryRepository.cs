using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Todo.Common.ApplicationContext;
using Todo.Common.Authorization;
using Todo.Persistence.IntegrationTests.Fixtures.TestEntities;
using Todo.Persistence.IntegrationTests.Fixtures.TestServices;
using Todo.Persistence.Repositories;
using Todo.Persistence.Repositories.Settings;

namespace Todo.Persistence.IntegrationTests.Fixtures.TestRepositories;

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