using WebScraperDll.Models;

namespace WebScraperDll
{
    public class LinkContainer(int maxScrapedPages)
    {
        public HashSet<string> ScrapedLinks { get; set; } = new();
        public int MaxScrapedPages { get; } = maxScrapedPages;
        public List<LinkItem> Links { get; } = new();


        /// <summary>
        ///     Link that is being scraped
        /// </summary>
        /// <param name="scrapedLink"></param>
        /// <param name="pageUri"></param>
        /// <returns></returns>
        private LinkItem FindOrAdd(string scrapedLink, Uri pageUri)
        {
            if (Links.TryFind(x => x.AbsoluteUri == scrapedLink, out var value))
            {
                return value;
            }

            value = new LinkItem(scrapedLink, pageUri);
            Links.Add(value);
            return value;
        }

        public void AddRoot(string absoluteUri)
        {
            // ReSharper disable once InlineTemporaryVariable
            var pageUri = absoluteUri; // for root, page is same as link (here for clarity)
            Links.Add(new LinkItem(absoluteUri, new Uri(pageUri)));
        }

        public void Add(string absoluteUri, string pageUrl)
        {
            var item = FindOrAdd(absoluteUri, new Uri(pageUrl));
            if (item.OnPage.TryGetValue(pageUrl, out var count))
            {
                item.OnPage[pageUrl] = count + 1;
                Console.WriteLine($"Multiple Times = {item.OnPage[pageUrl]} = on page = {pageUrl}");
            }
            else
            {
                item.OnPage[pageUrl] = 1;
            }
        }

        public LinkItem? GetNext()
        {
            var tmp = Links.FirstOrDefault(x => x.StatusCode == -1);
            if (tmp == null || ScrapedLinks.Count >= MaxScrapedPages)
            {
                return null;
            }
            ScrapedLinks.Add(tmp.AbsoluteUri);
            return tmp;
        }
    }
}
