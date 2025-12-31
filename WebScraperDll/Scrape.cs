using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public Scrape(string url)
    {
        RootUri = new LinkObj(url);
    }

    public HashSet<string> LinkHashSet { get; set; } = new();
    public LinkObj RootUri { get; }

    public List<LinkToScrape> LinksToScrape { get; set; } = new();

    public LinkList LinkList { get; } = new();


    public async Task Init(int maxScrape)
    {
        LinkList.AddRoot(RootUri.AbsoluteUri);
        while (LinkList.GetNext() is { } link && LinkHashSet.Count < maxScrape)
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
        var linkWasAdded = LinkHashSet.Add(linkItem.Uri.AbsoluteUri);

        
        
        var linkToScrape = new LinkToScrape(linkItem.Uri.AbsoluteUri);
        linkItem.SetWebResponseResult(await Requester.GetFromWeb(linkItem.Uri.AbsoluteUri));
        if (IsNotTheSameHost(linkItem.Uri))
        {
            return; // do not add links for other hosts
        }
        var htmlDoc = new HtmlDocHelper(linkItem.WebResponseResult.Content, linkItem.Uri.AbsoluteUri);
        ProcessLinks(htmlDoc, linkItem);
        LinksToScrape.Add(linkToScrape);
    }


    public void ProcessLinks(HtmlDocHelper htmlDoc, LinkItem linkItem)
    {
        if (!string.Equals(htmlDoc.Host, RootUri.Host, StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }

        foreach (var href in htmlDoc.InternalLinks)
        {
            Console.WriteLine(href);
            var absoluteUrl = new Uri(new Uri(RootUri.AbsoluteUri), href).AbsoluteUri;
            LinkList.Add(absoluteUrl, linkItem.Uri.AbsoluteUri);
        }
    }
}