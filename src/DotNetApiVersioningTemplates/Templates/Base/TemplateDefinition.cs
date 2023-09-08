namespace DotNetApiVersioningTemplates.Templates.Base;

public abstract class TemplateDefinition
{
    public string? BaseUrl { get; init; }
    public List<string> Files { get; } = new List<string>();
}