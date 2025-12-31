using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public Scrape(string url)
    {
        RootUri = new Uri(url);
    }

    public string Host { get; }
    public Uri RootUri { get; }

    public LinkList LinkList { get; } = new();

    public async Task Init()
    {
        LinkList.AddRoot(RootUri.AbsoluteUri);
        while (LinkList.GetNext() is { } link)
        {
            await DoEach(link);
        }
    }

    private bool IsNotTheSameHost(Uri uri)
    {
        return !uri.Host.Equals(RootUri.Host, StringComparison.OrdinalIgnoreCase);
    }

    private async Task DoEach(LinkItem linkItem)
    {
        linkItem.LinkScrapedFromWeb = true;
        Console.WriteLine($"DoEach :: {linkItem.AbsoluteUri}");
        linkItem.WebResponseResult = await Requester.GetFromWeb(linkItem.AbsoluteUri);
        if (IsNotTheSameHost(linkItem.Uri))
        {
            return; // do not add links for other hosts
        }

        var htmlDoc = new HtmlDocHelper(linkItem.WebResponseResult.Content);
        var links = htmlDoc.Links.ToList();
        foreach (var href in links.Select(link => link.Attributes["href"].Value).Where(href => !string.IsNullOrWhiteSpace(href)))
        {
            Console.WriteLine(href);
            var absoluteUrl = new Uri(RootUri, href).ToString();
            LinkList.Add(absoluteUrl, linkItem.AbsoluteUri);
            linkItem.Links.Add(href);
        }
    }
}