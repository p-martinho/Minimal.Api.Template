using FluentValidation;
using Microsoft.Extensions.Logging;
using Minimal.Api.Template.Application.Queries;
using Minimal.Api.Template.Application.Queries.Models;

namespace Minimal.Api.Template.Application.Tests.TestHandlers;

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