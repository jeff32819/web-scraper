using Newtonsoft.Json;
using WebScraperDll.Models;

namespace WebScraperDll;

public class Scrape
{


    public Scrape(string url, int maxScrape)
    {
        MaxPagesToScrape = maxScrape;
        RootUri = new LinkObj(url);
        ScrapeQueue = new ScrapeQueue();
        ScrapeQueue.Enqueue(RootUri.AbsoluteUri);
    }

    public int MaxPagesToScrape { get; }
    public LinkObj RootUri { get; }
    public LinkContainer LinkContainer { get; } = new();
    public List<PageItem> Pages { get; } = new();
    public ScrapeQueue ScrapeQueue { get; }
    public async Task Process()
    {
        while (ScrapeQueue.GetNext() is { } link)
        {

            Console.WriteLine($"Processing link: {link}");
            var pg = new PageItem(link);
            await pg.GetWebResponseResult();
            LinkContainer.Links.Add(new LinkItem(link, link)); // Add the current page's link to the container
            Pages.Add(pg);
            // Optional: Add checks to avoid infinite loops for pages with many links
            // Optional: Handle broken links or timeouts

            if (RootUri.Host != pg.PageHost)
            {
                continue;
            }

            if (ScrapeQueue.Count >= MaxPagesToScrape)
            {
                continue;
            }

            foreach (var linkItem in pg.Links)
            {
                ScrapeQueue.Enqueue(linkItem.LinkAbsoluteUri);
            }


            //await page.GetWebResponseResult();
            //foreach(var link in page.Links)
            //{
            //    Console.WriteLine(link.LinkAbsoluteUri);
            //    Console.WriteLine(link.IsInternalLink);
            //    Console.WriteLine("********************************************");
            //}

            //if (RootUri.Host != page.PageHost)
            //{
            //    continue;
            //}
            //foreach (var link in page.Links)
            //{
            //    ScrapeQueue.Enqueue(PageContainer.Add(link.LinkAbsoluteUri));
            //}




        }
        await File.WriteAllTextAsync("t:\\pages.txt", Newtonsoft.Json.JsonConvert.SerializeObject(Pages, Formatting.Indented));
        Console.WriteLine("done");
    }
}