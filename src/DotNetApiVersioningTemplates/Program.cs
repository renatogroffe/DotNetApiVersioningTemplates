using System.Text.RegularExpressions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using DotNetApiVersioningTemplates.Templates;
using DotNetApiVersioningTemplates.Templates.Menu;

var logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

Console.WriteLine();
var template = MenuTemplates.Execute();

Console.WriteLine();
string? projectName = null;
do
{
    if (projectName is not null)
    {
        var oldForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Inform a valid project name!");
        Console.ForegroundColor = oldForegroundColor;
        Console.WriteLine();
    }
    Console.Write("Project name: ");
    projectName = Console.ReadLine();
} while (String.IsNullOrWhiteSpace(projectName) ||
         !Regex.IsMatch(projectName, @"^(?:[a-zA-Z_][a-zA-Z0-9_]*(?:\.[a-zA-Z_][a-zA-Z0-9_]*)*)?$"));

try
{
    Console.WriteLine();
    logger.Information("Starting the project generation...");
    await ProjectGenerator.GenerateProject(projectName, template, logger);
    logger.Information($"Project * {projectName} * with template * {template} * generated successfully!");
}
catch (Exception ex)
{
    logger.Error(ex, "Error generating project");
    return;
}