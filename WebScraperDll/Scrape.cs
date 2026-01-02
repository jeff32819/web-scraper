using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{
    public HashSet<string> PageScrapedHashSet = new();

    public Scrape(string url, int maxScrape)
    {
        MaxPagesToScrape = maxScrape;
        RootUri = new LinkObj(url);
        ScrapeQueue = new ScrapeQueue();
        var pg = new PageItem(RootUri.AbsoluteUri);
        PageContainer.Add(pg);
        ScrapeQueue.Enqueue(pg);

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
            PageScrapedHashSet.Add(page.PageUri);
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
                var pg = new PageItem(link.LinkAbsoluteUri);
                PageContainer.Add(pg);
                ScrapeQueue.Enqueue(pg);
            }
        }
        
        Console.WriteLine("done");
    }
}