# Template Developer Notes

This solution is for a .NET solution template.
It includes the source files (the files that will be in the output solution) and the template files (files that belongs to the template, not to its output).

# Template Files

- `Minimal.Api.Template.csproj`: To build a template package (NuGet `.nupkg` file that bundles one or more templates together), we need a C# project file (`.csproj`) configured to act as a packaging project rather than a compilation project. The file has specific properties for templating.
- `.template.config/template.json`: file with the template configuration.
- `README.md`: the template's README that explains how to create a solution with the template and all the information about the projects, architecture decisions and technologies.
- `README-template.md`: The README that will exist in the template output. Basically, the template engine will copy it and then rename it to `README.md`. The template's `README.md` is not copied.
- `docs/**`: files used to document the template (e.g. diagrams used in the template's README).
- `icon.png`: the icon for the NuGet package.

# Template Configuration

The template is configured in the file `.template.config/template.json`, namely the symbols, parameters, what to include or exclude, post actions, etc.

Some of the configurations used:
- `sources`: include/exclude the files that will be created. For instance, it excludes template specific files, that we don't want to include in the template output.
- `guids`: list of GUIDs which appear in the template source and should be replaced in the template output. For each GUID listed, a replacement GUID is generated, and replaces all occurrences of the source GUID in the output. This is useful to have different GUIDs for each time the template is used.

# Parameters

- `-n` or `--name`: This is an optional parameter from `dotnet new <TEMPLATE>` command, not a parameter from this template. It is the name for the created output. If no name is specified, the name of the current directory is used. This name will be used to replace `Minimal.Api.Template` string everywhere.
- `with-docker`: adds support for Docker in the new solution or new module. The template output will include Docker-related files (like `Dockerfile`and `docker-compose.yml`). Without this option, everything inside `#if (IsToAddDocker)` directives will be excluded from the output.
- `with-identity`: adds identity API endpoints and users storage (the API will be able to manage users and tokens). The template output will include code and files for that. Without this option, everything inside `#if (!IsToExcludeIdentity)` directives will be excluded from the output.

# Re-create Initial Migrations

To re-do the initial migrations:

- Remove the folders 'Migrations' from projects `Minimal.Api.Template.Pesistence` and `Minimal.Api.Template.Persistence.IntegrationTests`
- Temporally remove `Minimal.Api.Template.csproj` (its existence along with the solution file will cause an error in the migration command)
- Create new migrations, using the commands:
  ```
  dotnet ef migrations add InitialMigration --startup-project ./src/Minimal.Api.Template.Presentation.Api/ --project ./src/Minimal.Api.Template.Persistence/ --context ApplicationDbContext -- --environment Migration
  dotnet ef migrations add InitialMigration --startup-project ./src/Minimal.Api.Template.Presentation.Api/ --project ./src/Minimal.Api.Template.Persistence/ --context IdentityDbContext --output-dir Migrations/Identity -- --environment Migration
  dotnet ef migrations add InitialMigration --startup-project ./tests/Minimal.Api.Template.Persistence.IntegrationTests/ --project ./tests/Minimal.Api.Template.Persistence.IntegrationTests/
  ```
- Revert the removal of `Minimal.Api.Template.csproj`

# Test Template Locally

Before shipping the NuGet package, we need to test the template.
The best way to check the output is installing the template directly from its folder and then test it:

- Uninstall the template, if installed normally (with package): `dotnet new uninstall PMart.Minimal.Api.Template`
- Install/reinstall from local folder (in the root of the solution): `dotnet new install .\ --force`
- Test it: `dotnet new min-api -n YourSolutionName`
- In the end, uninstall it:
    - Check the command to uninstall it: `dotnet new uninstall`
    - Run the uninstallation command (instead of the name of the template, it uses the template local full path)

# Publish Template

- Increment the `PackageVersion` in `Modular.Api.Template.csproj`.
- Merge Pull Request to main branch, it will trigger the GitHub action `publish.yaml`.
- Add new release in GitHub.

# References

- [.NET templates for authors](https://learn.microsoft.com/en-us/dotnet/core/tools/templates)
- [.NET Templating Wiki](https://github.com/dotnet/templating/wiki)
- [Tutorial: Create a project template](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template)