# Developer Notes

- In the template.json, the src/Directory.Build.props is copy only, because template engine will process the condition in there and the section will be empty in the final project.

To install template locally:

- Unninstall the template, if installed normally (with package): dotnet new uninstall PMart.Minimal.Api.Template
- Install/reinstall from local foder (in the root of the solution): dotnet new install .\ --force

To create certificates:

``` shell
dotnet dev-certs https -ep %appdata%\ASP.NET\Https\Minimal.Api.Template.Presentation.Api.pfx -p 0dd49423-8bfd-44a9-a909-ad4d4b96e359
dotnet dev-certs https --trust
dotnet user-secrets -p ./src/Minimal.Api.Template.Apresentation.Api/Minimal.Api.Template.Presentation.Api.csproj set "Kestrel:Certificates:Development:Password" "0dd49423-8bfd-44a9-a909-ad4d4b96e359"
```