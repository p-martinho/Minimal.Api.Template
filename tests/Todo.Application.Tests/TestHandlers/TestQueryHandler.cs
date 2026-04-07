using FluentValidation;
using Microsoft.Extensions.Logging;
using Todo.Application.Queries;
using Todo.Application.Queries.Models;

namespace Todo.Application.Tests.TestHandlers;

internal class TestQueryHandler : QueryHandler<string, string>
{
    public TestQueryHandler(ILogger logger, IValidator<string> validator) : base(logger, validator)
    {
    }

    protected override Task<QueryOut<string>> HandleQueryInAsync(string queryIn, CancellationToken cancellationToken)
    {
        return Task.FromResult(QueryOut<string>.Success("QueryOut"));
    }
}