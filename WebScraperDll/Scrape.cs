using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public Scrape(string url)
    {
        Url = new Uri(url);
        
    }
    
    public string Host { get; } 
    public Uri Url { get; } 
   
    public WebScraperDll.LinkList LinkList { get; } = new WebScraperDll.LinkList();

    public async Task Init()
    {
        LinkList.Add(Url.AbsoluteUri, "");
        while (LinkList.GetNext() is { } link)
        {
            await DoEach(link);
        }
    }

    private int PageCount { get; set; } = 0;
    
    private async Task DoEach(Models.LinkItem linkItem)
    {
        linkItem.LinkScrapedFromWeb = true;
        
        Console.WriteLine($"DoEach :: {linkItem.AbsoluteUri}");
        PageCount++;
        if (PageCount > 20)
        {
            return;
        }
        var response = await Requester.GetFromWeb(linkItem.AbsoluteUri);
        var htmlDoc = new HtmlDocHelper(response.Content);
        foreach (var href in htmlDoc.Links.Select(link => link.Attributes["href"].Value).Where(href => !string.IsNullOrWhiteSpace(href)))
        {
            Console.WriteLine(href);
            var absoluteUrl = new Uri(Url, href).ToString();
            LinkList.Add(absoluteUrl, linkItem.AbsoluteUri);
        }
    }
}