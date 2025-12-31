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
    public HashSet<string> LinksWithoutContent { get; set; } = new();
    public LinkObj RootUri { get; }
    public LinkList LinkList { get; } = new();

    public async Task Init(int maxScrape)
    {
        LinkList.AddRoot(RootUri.AbsoluteUri);
        while (LinkList.GetNext() is { } link && LinkHashSet.Count < maxScrape)
        {
            await DoEach(link);
        }
    }


    private async Task DoEach(LinkItem linkItem)
    {
        var linkWasAdded = LinkHashSet.Add(linkItem.AbsoluteUri);
        linkItem.SetWebResponseResult(await Requester.GetFromWeb(linkItem.AbsoluteUri));
        if (Code.SameHost(RootUri.AbsoluteUri, linkItem.AbsoluteUri))
        {
            return; // do not add links for other hosts
        }

        if (linkItem.WebResponseResult == null)
        {
            LinksWithoutContent.Add(linkItem.AbsoluteUri);
            return;
        }

        var htmlDoc = new HtmlDocHelper(linkItem.WebResponseResult.Content, linkItem.AbsoluteUri);
        ProcessLinks(htmlDoc, linkItem);
    }


    public void ProcessLinks(HtmlDocHelper htmlDoc, LinkItem linkItem)
    {
        // check if the site is from the same host as the root.
        if (!string.Equals(htmlDoc.Host, RootUri.Host, StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }

        // process internal links, add them to the link list. 
        // was checked above to make sure they are from the same host.
        foreach (var href in htmlDoc.InternalLinks)
        {
            Console.WriteLine(href);
            var absoluteUrl = new Uri(new Uri(RootUri.AbsoluteUri), href).AbsoluteUri;
            LinkList.Add(absoluteUrl, linkItem.AbsoluteUri);
        }
    }
}