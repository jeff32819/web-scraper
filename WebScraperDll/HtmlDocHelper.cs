using HtmlAgilityPack;

namespace WebScraperDll;

public class HtmlDocHelper
{
    public HtmlDocHelper(string html, string pageAbsoluteUri)
    {
        Console.WriteLine($"HtmlDocHelper = pageAbsoluteUri = {pageAbsoluteUri}");
        var pageUri = pageAbsoluteUri.ToUri();
        Host = pageUri.Host;
        Document.LoadHtml(html);
        var links = Document.DocumentNode.SelectNodes("//a[@href]");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        // selectnodes can actually be null, but the signature says it cannot
        if (links == null)
        {
            return;
        }

        foreach (var link in links)
        {
            Links.Add(link.GetAttributeValue("href", string.Empty));
        }
    }

    public HtmlDocument Document { get; } = new();
    public string Host { get; }
    public List<string> Links { get; set; } = new();
}