using System.Reflection;

namespace Architecture.Tests.Helpers;

/// <summary>
/// Patterns for specific namespaces.
/// </summary>
internal static class Namespaces
{
    public const string System = "System";

    public const string Microsoft = "Microsoft";

    public const string MicrosoftEfCore = "Microsoft.EntityFrameworkCore";

    public static readonly string Presentation = GetNamespaceFromAssembly(Assemblies.Presentation);

    public static readonly string Application = GetNamespaceFromAssembly(Assemblies.Application);

    public static readonly string Persistence = GetNamespaceFromAssembly(Assemblies.Persistence);

    public static readonly string Domain = GetNamespaceFromAssembly(Assemblies.Domain);

    public static readonly string Common = GetNamespaceFromAssembly(Assemblies.Common);

    public const string PatternForEndpointGroup = ".Presentation.Api.Endpoints";

    public const string PatternForCommandHandler = ".Application.Commands";

    public const string PatternForQueryHandler = ".Application.Queries";

    public const string PatternForRepository = ".Persistence.Repositories";

    public const string PatternForEntityTypeConfiguration = ".Persistence.Configurations";

    public const string PatternForEntities = ".Domain.Entities";

    private static string GetNamespaceFromAssembly(Assembly assembly)
    {
        return assembly.GetName().Name!;
    }
}