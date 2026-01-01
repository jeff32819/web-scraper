using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public Scrape(string url, int maxScrape)
    {
        LinkList = new LinkContainer(maxScrape);
        RootUri = new LinkObj(url);
    }


    public HashSet<string> LinksWithoutContent { get; set; } = new();
    public LinkObj RootUri { get; }
    public LinkContainer LinkList { get; }

    public async Task Init()
    {
        LinkList.AddRoot(RootUri.AbsoluteUri);
        while (LinkList.GetNext() is { } link)
        {
            await DoEach(link);
        }
    }


    private async Task DoEach(LinkItem linkItem)
    {
        linkItem.SetWebResponseResult(await Requester.GetFromWeb(linkItem.LinkAbsoluteUri));
        ProcessLinks(linkItem);
    }

    public void ProcessLinks(LinkItem linkItem)
    {
        if (linkItem.WebResponseResult == null)
        {
            LinksWithoutContent.Add(linkItem.LinkAbsoluteUri);
            return;
        }

        var htmlDoc = new HtmlDocHelper(linkItem.WebResponseResult.Content, linkItem.PageAbsoluteUri);
        // check if the site is from the same host as the root.
        Console.WriteLine($"is same host {linkItem.LinkAbsoluteUri} -- {RootUri.AbsoluteUri}");
        if (linkItem.VerifyLinksOnly)
        {
            return;
        }

        // process internal links, add them to the link list. 
        // was checked above to make sure they are from the same host.
        foreach (var relativeUri in htmlDoc.AllLinks)
        {
            Console.WriteLine(relativeUri);

            LinkList.Add(relativeUri, linkItem);
        }
    }
}