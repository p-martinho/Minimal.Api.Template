using FluentValidation;
using Microsoft.Extensions.Logging;
using Todo.Application.Commands;
using Todo.Application.Commands.Models;

namespace Todo.Application.Tests.TestHandlers;

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