using System.Text;
using Serilog.Core;
using DotNetApiVersioningTemplates.Templates.Base;
using DotNetApiVersioningTemplates.Templates.Options;

namespace DotNetApiVersioningTemplates.Templates;

public static class ProjectGenerator
{
    public static async Task GenerateProject(string projectName, TemplateOptions template,
        Logger logger)
    {
        string projectDirectory;
        #if DEBUG
            var testsDirectory = Path.Combine(
                new DirectoryInfo(Environment.CurrentDirectory).Parent!.FullName, "Tests");
            if (!Directory.Exists(testsDirectory))
                Directory.CreateDirectory(testsDirectory);
            projectDirectory = Path.Combine(testsDirectory, projectName);
        #else
            projectDirectory = Path.Combine(Environment.CurrentDirectory, projectName);
        #endif

        if (Directory.Exists(projectDirectory))
            throw new Exception("A directory with this project name already exists. Please enter a different project name!");
        Directory.CreateDirectory(projectDirectory);
        
        TemplateDefinition templateDefinition;
        switch (template)
        {
            case TemplateOptions.WebApiDotNet7:
                templateDefinition = new WebApiDotNet7();
                break;
            case TemplateOptions.MinimalApisDotNet7:
                templateDefinition = new MinimalApisDotNet7();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(template), template, null);
        }
        var httpClient = new HttpClient();
        foreach (var file in templateDefinition.Files)
        {
            var fileUrl = new Uri(new Uri(templateDefinition.BaseUrl! + "/"), file).ToString();
            var fileName = Path.GetFileName(fileUrl);
            var response = await httpClient.GetAsync(fileUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var filePath = Path.Combine(projectDirectory, file);
                Directory.CreateDirectory(new FileInfo(filePath).DirectoryName!);
                var fileContent = new StringBuilder(content);
                fileContent.Replace("Groffe", projectName);
                if (filePath.EndsWith(".csproj"))
                    filePath = filePath.Replace("Groffe.csproj", projectName + ".csproj");
                File.WriteAllText(filePath, fileContent.ToString());
                logger.Information($"{filePath} created...");
            }
            else
            {
                Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}