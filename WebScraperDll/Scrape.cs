using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{


    public Scrape(string url, int maxScrape)
    {
        MaxPagesToScrape = maxScrape;
        RootUri = new LinkObj(url);
        ScrapeQueue = new ScrapeQueue();
        ScrapeQueue.Enqueue(PageContainer.Add(RootUri.AbsoluteUri));
    }

    public int MaxPagesToScrape { get; }
    public LinkObj RootUri { get; }
    public LinkContainer LinkContainer { get; } = new();
    public PageContainer PageContainer { get; } = new();
    public ScrapeQueue ScrapeQueue { get; }
    public async Task Process()
    {
        while (ScrapeQueue.GetNext() is { } page)
        {
            await page.GetWebResponseResult();
            foreach(var link in page.Links)
            {
                Console.WriteLine(link.LinkAbsoluteUri);
                Console.WriteLine(link.IsInternalLink);
                Console.WriteLine("********************************************");
            }

            if (RootUri.Host != page.PageHost)
            {
                continue;
            }
            foreach (var link in page.Links)
            {
                ScrapeQueue.Enqueue(PageContainer.Add(link.LinkAbsoluteUri));
            }
        }
        
        Console.WriteLine("done");
    }
}