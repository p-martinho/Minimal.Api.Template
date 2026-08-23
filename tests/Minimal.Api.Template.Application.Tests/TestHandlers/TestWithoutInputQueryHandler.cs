using Microsoft.Extensions.Logging;
using Minimal.Api.Template.Application.Queries;
using Minimal.Api.Template.Application.Queries.Models;

namespace Minimal.Api.Template.Application.Tests.TestHandlers;

internal class TestWithoutInputQueryHandler : QueryHandler<string>
{
    private readonly ITestService _testService;

    public TestWithoutInputQueryHandler(ILogger logger,
        ITestService testService)
        : base(logger)
    {
        _testService = testService;
    }

    protected override Task<QueryOut<string>> ExecuteAsync(CancellationToken cancellationToken)
    {
        _testService.DoSomething();

        return Task.FromResult(QueryOut<string>.Success("QueryOut"));
    }
}