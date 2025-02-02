using System;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;
public class TestSuite : TestBase
{
    private IPage _page;
    private IBrowser _browser;
    private IPlaywright _playwright;
    [SetUp]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _page = await _browser.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [Test]
    [TestCase("Web Application", "To Do", "Implement user authentication", new[] { "Feature", "High Priority" })]
    [TestCase("Web Application", "To Do", "Fix navigation bug", new[] { "Bug" })]
    [TestCase("Web Application", "In Progress", "Design system updates", new[] { "Design" })]
    [TestCase("Mobile Application", "To Do", "Push notification system", new[] { "Feature" })]
    [TestCase("Mobile Application", "In Progress", "Offline mode", new[] { "Feature", "High Priority" })]
    [TestCase("Mobile Application", "Done", "App icon design", new[] { "Design" })]
    public async Task ValidateTaskColumns(string appName, string columnName, string taskName, string[] tags)
    {
        await DoLogin(_page);
        await NavigateToApp(appName,_page);
        await VerifyTaskInColumn(taskName, columnName, tags, _page);
    }
}
