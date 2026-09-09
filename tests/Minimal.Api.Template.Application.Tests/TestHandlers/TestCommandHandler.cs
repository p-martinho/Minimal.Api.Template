using FluentValidation;
using Microsoft.Extensions.Logging;
using Minimal.Api.Template.Application.Commands;
using Minimal.Api.Template.Application.Commands.Models;

namespace Minimal.Api.Template.Application.Tests.TestHandlers;

internal class TestCommandHandler : CommandHandler<string, string>
{
    public TestCommandHandler(ILogger logger, IValidator<string> validator) : base(logger, validator)
    {
    }

    protected override Task<CommandOut<string>> HandleCommandInAsync(string commandIn,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(CommandOut<string>.Success("CommandOut"));
    }
}