using DotNetApiVersioningTemplates.Templates.Base;

namespace DotNetApiVersioningTemplates.Templates.Options;

public class MinimalApisDotNet7 : TemplateDefinition
{
    public MinimalApisDotNet7()
    {
        BaseUrl = "https://raw.githubusercontent.com/renatogroffe/DotNetApiVersioningTemplates/main/templates/1.00.00/dotnet7/minimal/Groffe";
        Files.Add("Groffe.csproj");
        Files.Add("Program.cs");
        Files.Add("Contador.cs");
        Files.Add("appsettings.json");
        Files.Add("appsettings.Development.json");
        Files.Add("Properties/launchSettings.json");
        Files.Add("Versioning/ConfigureSwaggerOptions.cs");
        Files.Add("Versioning/SwaggerDefaultValues.cs");
        Files.Add("V1/Models/ResultadoContador.cs");
        Files.Add("V1/Endpoints/ContagemEndpoints.cs");
        Files.Add("V2/Models/ResultadoContador.cs");
        Files.Add("V2/Endpoints/ContagemEndpoints.cs");
    }
}