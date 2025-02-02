using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

public class TestBase
{
    private readonly IConfiguration _config;

    public TestBase()
    {
        // Set the base path to the directory where the source code is located
        var currentDirectory = Directory.GetCurrentDirectory();

        _config = new ConfigurationBuilder()
            .SetBasePath(currentDirectory)  // Set base path to the directory where the .cs files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public async Task DoLogin(IPage page)
    {
        // Accessing the configuration properties directly
        string? url = _config["TestSettings:Url"]; ;  // The Url property from appsettings
        string? username = _config["TestSettings:Username"]; ;  // The Username property from appsettings
        string? password = _config["TestSettings:Password"]; ;  // The Password property from appsettings

        await page.GotoAsync(url);
        await page.FillAsync("#username", username);
        await page.FillAsync("#password", password);
        await page.GetByRole(AriaRole.Button, new() { NameString="Sign In"}).ClickAsync();
    }

    public async Task NavigateToApp(string appName, IPage _page)
    {
        await _page.GetByRole(AriaRole.Button, new() { NameString=appName}).ClickAsync();
    }

    public async Task VerifyTaskInColumn(string cardName, string columnName, string[] expectedTags, IPage _page)
    {
        // Locate column
        var columnIndex = columnName switch
        {
            "To Do" => 1,
            "In Progress" => 2,
            "Review" => 3,
            "Done" => 4,
            _ => throw new ArgumentException("Invalid column name")
        };

        var column = _page.Locator($"div:nth-child({columnIndex})");
        Assert.That(await column.GetByText(columnName).IsVisibleAsync(), $"{columnName} Column not available");

        // Find the card within the column
        var card = column.Locator("div").Filter(new() { HasText = cardName }).First;
        Assert.That(await card.IsVisibleAsync(), $"{cardName} task not found");

        // Validate tags
        foreach (var tag in expectedTags)
        {
            Assert.That(await card.GetByText(tag).First.IsVisibleAsync(), $"Tag '{tag}' not found for {cardName}");
        }
    }
}
