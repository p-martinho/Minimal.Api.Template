using System.Reflection;
using Minimal.Api.Template.Application.Commands;
using Minimal.Api.Template.Common.ApplicationContext;
using Minimal.Api.Template.Domain.Entities;
using Minimal.Api.Template.Persistence.Repositories;

namespace Architecture.Tests.Helpers;

/// <summary>
/// Assemblies references for every module (including SharedCore assemblies).
/// </summary>
/// <remarks>Add here the assemblies for each module.</remarks>
internal static class Assemblies
{
    public static readonly Assembly Presentation = typeof(Program).Assembly;

    public static readonly Assembly Application = typeof(ICommandHandler<>).Assembly;

    public static readonly Assembly Persistence = typeof(IRepository<>).Assembly;

    public static readonly Assembly Domain = typeof(BaseEntity).Assembly;

    public static readonly Assembly Common = typeof(ICurrentUser).Assembly;

    public static readonly Assembly[] All = [Presentation, Application, Persistence, Domain, Common];

    public static Assembly[] GetAllExcept(params Assembly[] assemblies)
    {
        return All.Where(a => !assemblies.Contains(a)).ToArray();
    }
}