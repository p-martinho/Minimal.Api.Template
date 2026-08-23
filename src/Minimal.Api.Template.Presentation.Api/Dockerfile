FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Directory.Build.props", "src/"]
COPY ["Directory.Build.props", "."]
COPY ["src/Minimal.Api.Template/Minimal.Api.Template.Presentation.Api/Minimal.Api.Template.Presentation.Api.csproj", "src/Minimal.Api.Template/Minimal.Api.Template.Presentation.Api/"]
COPY ["src/Aspire/Aspire.ServiceDefaults/Aspire.ServiceDefaults.csproj", "src/Aspire/Aspire.ServiceDefaults/"]
COPY ["src/Minimal.Api.Template/Minimal.Api.Template.Application/Minimal.Api.Template.Application.csproj", "src/Minimal.Api.Template/Minimal.Api.Template.Application/"]
COPY ["src/Minimal.Api.Template/Minimal.Api.Template.Persistence/Minimal.Api.Template.Persistence.csproj", "src/Minimal.Api.Template/Minimal.Api.Template.Persistence/"]
COPY ["src/Minimal.Api.Template/Minimal.Api.Template.Domain/Minimal.Api.Template.Domain.csproj", "src/Minimal.Api.Template/Minimal.Api.Template.Domain/"]
COPY ["src/Minimal.Api.Template/Minimal.Api.Template.Common/Minimal.Api.Template.Common.csproj", "src/Minimal.Api.Template/Minimal.Api.Template.Common/"]
RUN dotnet restore "./src/Minimal.Api.Template/Minimal.Api.Template.Presentation.Api/Minimal.Api.Template.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/src/Minimal.Api.Template/Minimal.Api.Template.Presentation.Api"
RUN dotnet build "./Minimal.Api.Template.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Minimal.Api.Template.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minimal.Api.Template.Presentation.Api.dll"]