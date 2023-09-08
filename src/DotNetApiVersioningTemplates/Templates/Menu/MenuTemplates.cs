using DustInTheWind.ConsoleTools.Controls.Menus;
using DotNetApiVersioningTemplates.Templates.Options;

namespace DotNetApiVersioningTemplates.Templates.Menu;

public static class MenuTemplates
{
    public static TemplateOptions Execute()
    {
        var textMenu = new TextMenu
        {
            TitleText = "**** Creating a REST API with versioning | © Renato Groffe - 2023 - MIT License ****",
            ForegroundColor = ConsoleColor.Green,
            EraseAfterClose = false
        };
        textMenu.AddItems(new List<TextMenuItem>
        {
            new TextMenuItem
            {
                Id = $"{(int)(TemplateOptions.WebApiDotNet7)}",
                Text = "ASP.NET Core Web API in .NET 7"
            },
            new TextMenuItem
            {
                Id = $"{(int)(TemplateOptions.MinimalApisDotNet7)}",
                Text = "ASP.NET Core Minimal APIs in .NET 7"
            }
        });
        textMenu.QuestionText = "Enter the template number of your choice: ";
        textMenu.InvalidOptionText = "Invalid option. Please select a valid number.";
        textMenu.Display();
        return (TemplateOptions)Enum.Parse(typeof(TemplateOptions), textMenu.SelectedItem.Id);
    }
}