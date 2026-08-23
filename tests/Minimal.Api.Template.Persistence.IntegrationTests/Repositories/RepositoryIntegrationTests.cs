using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestEntities;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestRepositories;
using Minimal.Api.Template.Persistence.IntegrationTests.Fixtures.TestServices;
using Minimal.Api.Template.Persistence.Repositories.Settings;
using NSubstitute;

namespace Minimal.Api.Template.Persistence.IntegrationTests.Repositories;

public sealed class RepositoryIntegrationTests : IClassFixture<TestDataEfCoreFixture>, IDisposable
{
    private readonly TestRepository _repository;
    private readonly IServiceScope _testScope;
    private readonly TestDbContext _context;

    public RepositoryIntegrationTests(TestDataEfCoreFixture fixture)
    {
        _testScope = fixture.ServiceProvider.CreateScope();
        _context = _testScope.ServiceProvider.GetRequiredService<TestDbContext>();
        
        var queryParametersOptions = Substitute.For<IOptionsSnapshot<QueryParametersSettings>>();
        queryParametersOptions.Value.Returns(new QueryParametersSettings());

        _repository = new TestRepository(_context, queryParametersOptions);
    }

    [Fact]
    public async Task AddAsync_ShouldSucceed()
    {
        // Arrange
        var entity = new TestEntity { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() };

        // Act
        await _repository.AddAsync(entity, TestContext.Current.CancellationToken);

        // Assert
        Assert.Contains(_context.ChangeTracker.Entries<TestEntity>(), e => e.Entity == entity);
    }

    [Fact]
    public async Task AddRangeAsync_ShouldSucceed()
    {
        // Arrange
        IList<TestEntity> entities =
        [
            new() { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() },
            new() { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() }
        ];

        // Act
        await _repository.AddRangeAsync(entities, TestContext.Current.CancellationToken);

        // Assert
        foreach (var entity in entities)
        {
            Assert.Contains(_context.ChangeTracker.Entries<TestEntity>(), e => e.Entity == entity);
        }
    }

    [Fact]
    public async Task RemoveAsync_ShouldSucceed()
    {
        // Arrange
        var entity = new TestEntity { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() };
        await _repository.AddAsync(entity, TestContext.Current.CancellationToken);
        await _repository.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        await _repository.RemoveAsync(entity, TestContext.Current.CancellationToken);

        // Assert
        var entry = _context.Entry(entity);
        Assert.Equal(EntityState.Deleted, entry.State);
    }

    [Fact]
    public async Task RemoveRangeAsync_ShouldSucceed()
    {
        // Arrange
        IList<TestEntity> entities =
        [
            new() { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() },
            new() { OwnerId = "OwnerId", Code = Guid.NewGuid().ToString() }
        ];
        await _repository.AddRangeAsync(entities, TestContext.Current.CancellationToken);
        await _repository.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        await _repository.RemoveRangeAsync(entities, TestContext.Current.CancellationToken);

        // Assert
        foreach (var entity in entities)
        {
            var entry = _context.Entry(entity);
            Assert.Equal(EntityState.Deleted, entry.State);
        }
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldSucceed()
    {
        // Arrange
        var code = Guid.NewGuid().ToString();
        var entity = new TestEntity { OwnerId = "OwnerId", Code = code };
        await _repository.AddAsync(entity, TestContext.Current.CancellationToken);

        // Act
        await _repository.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Assert
        _context.ChangeTracker.Clear();
        var fetchedEntity =
            await _repository.FirstOrDefaultAsync(e => e.Code == code, TestContext.Current.CancellationToken);
        Assert.NotNull(fetchedEntity);
        Assert.NotSame(entity, fetchedEntity);
    }
    
    public void Dispose()
    {
        _testScope.Dispose();
    }
}