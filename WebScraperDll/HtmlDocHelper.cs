using HtmlAgilityPack;

namespace WebScraperDll;

public class HtmlDocHelper
{
    public HtmlDocHelper(string html, string pageAbsoluteUri)
    {
        Console.WriteLine($"HtmlDocHelper = pageAbsoluteUri = {pageAbsoluteUri}");
        var pageUri = pageAbsoluteUri.ToUri();
        Document.LoadHtml(html);
        var links = Document.DocumentNode.SelectNodes("//a[@href]");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        // selectnodes can actually be null, but the signature says it cannot
        if (links == null)
        {
            return;
        }

        RawLinks = links.ToList();
        foreach (var link in RawLinks)
        {
            Links.Add(new LinkItem
            {
                Href = link.GetAttributeValue("href", string.Empty),
                OuterHtml = link.OuterHtml,
                InnerHtml = link.InnerHtml,
            });
        }
    }

    public HtmlDocument Document { get; } = new();
    public List<LinkItem> Links { get; set; } = new();
    public List<HtmlNode> RawLinks { get; set; } = new();


    public class LinkItem
    {
        public string Href { get; set; } = string.Empty;
        public string OuterHtml { get; set; } = string.Empty;
        public string InnerHtml { get; set; } = string.Empty;
    }
}