using System.Diagnostics;
using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public Scrape(string url)
    {
        RootUri = new LinkObj(url);
    }
    public LinkObj RootUri { get; }

    public List<LinkToScrape> LinksToScrape { get; set; } = new();
    
    public LinkList LinkList { get; } = new();

    public async Task Init()
    {
        LinkList.AddRoot(RootUri.AbsoluteUri);
        //while (LinkList.GetNext() is { } link)
        //{
        ////    await DoEach(link);
        //}
        await DoEach(LinkList.Links.First());
        Debug.Print("flkjdsflkdsjfldskjf");
        
        
    }

    private bool IsNotTheSameHost(Uri uri)
    {
        return !uri.Host.Equals(RootUri.Host, StringComparison.OrdinalIgnoreCase);
    }

    private async Task DoEach(LinkItem linkItem)
    {

        var linkToScrape = new LinkToScrape(linkItem.Uri.AbsoluteUri);
        
        linkItem.LinkScrapedFromWeb = true;
        Console.WriteLine($"DoEach :: {linkItem.Uri.AbsoluteUri}");
        linkItem.WebResponseResult = await Requester.GetFromWeb(linkItem.Uri.AbsoluteUri);
        if (IsNotTheSameHost(linkItem.Uri))
        {
            return; // do not add links for other hosts
        }

        var htmlDoc = new HtmlDocHelper(linkItem.WebResponseResult.Content, linkItem.Uri.AbsoluteUri);
        //var links = htmlDoc.Links.ToList();
        //foreach (var href in links.Select(link => link.Attributes["href"].Value).Where(href => !string.IsNullOrWhiteSpace(href)))
        //{
        //    Console.WriteLine(href);
        //    var absoluteUrl = new Uri(new Uri(RootUri.AbsoluteUri), href).AbsoluteUri;
        //    LinkList.Add(absoluteUrl, linkItem.Uri.AbsoluteUri);
        //    linkItem.Links.Add(href);
        //    linkToScrape.Links.Add(new LinkObj(absoluteUrl));
        //}
        LinksToScrape.Add(linkToScrape);
    }
}