# Minimal.Api.Template

This solution was generated using the template [PMart.Minimal.Api.Template](https://github.com/p-martinho/Minimal.Api.Template).
Check the documentation [here](https://github.com/p-martinho/Minimal.Api.Template).

This is a simple and clean ASP.NET Core API.

# Requirements

* [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) (or later)
* [Docker Desktop](https://www.docker.com/)
* [EF Core CLI Tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

# Run

Run the `Aspire.AppHost` project. It requires **Docker Desktop** running, for the database.

The **Aspire Dashboard** will be launched automatically.

Navigate to [https://localhost:8217/scalar]() to see the **Todo API** documentation.

You can test the API, using the examples provided in the `.http` file (in the Presentation.Api folder).

# Test

Run the tests using the IDE or with the [command line](https://xunit.net/docs/getting-started/v3/cmdline):

```
dotnet test --solution YourSolutionName.slnx
```

The integration tests use a real database, using the [TestContainers](https://dotnet.testcontainers.org/) library (requires **Docker Desktop** running).

To assess the code coverage, and if your IDE does not include a tool for it, follow these instructions:

1. Install (if not already) the **ReportGenerator** tool:

    ``` bash
    dotnet tool install dotnet-reportgenerator-globaltool --global
    ```

2. Remove (if existent) the folder `TestResults` in the **root folder** (it contains previous coverage files)

3. Run the tests with code coverage enabled. Run this command in the **root folder** of the solution:

    ``` bash
    dotnet test --solution YourSolutionName.slnx --coverage --coverage-output-format cobertura --coverage-settings ./tests/CodeCoverage-settings.xml
    ```

4. Use the **ReportGenerator** tool to create HTML from the XML coverage files. Run this command in the **root folder** of the solution:

    ``` bash
    ReportGenerator -reports:./TestResults/*.cobertura.xml -targetdir:CoverageReport
    ```

5. Open the HTML file `CoverageReport/index.html` to see the results.

# EF Core Migrations

If you change the EF Core model (e.g., add a new property to an entity, or add a new entity), and you try to run the application, you will get an error: you have pending changes.
You need to create a new migration.

To create a migration, you need to have installed the [EF Core CLI Tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet). Then, in the root of the solution, run the following command:

```
dotnet ef migrations add <MigrationName> --startup-project ./src/YourSolutionName.Presentation.Api/ --project ./src/YourSolutionName.Persistence/ --context ApplicationDbContext -- --environment Migration
```

For the identity context (only applicable to solutions with identity), run this:

```
dotnet ef migrations add <MigrationName> --startup-project ./src/YourSolutionName.Presentation.Api/ --project ./src/YourSolutionName.Persistence/ --context IdentityDbContext --output-dir Migrations/Identity -- --environment Migration
```

> **Note:** The `--environment Migration` parameter is used to the pending migrations not being applied, which is the default in the `Development` environment.

If you ever need to add migrations to the `Todo.Persistence.IntegrationTests`, this would be the command:

```
dotnet ef migrations add <MigrationName> --startup-project ./tests/YourSolutionName.Persistence.IntegrationTests/ --project ./tests/YourSolutionName.Persistence.IntegrationTests/
```