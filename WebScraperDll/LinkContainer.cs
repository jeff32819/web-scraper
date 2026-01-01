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
        /// <param name="onPage"></param>
        /// <returns></returns>
        private LinkItem FindOrAdd(string scrapedLink, LinkItem onPage)
        {
            if (Links.TryFind(x => x.LinkAbsoluteUri == scrapedLink, out var value))
            {
                return value;
            }
            value = new LinkItem(scrapedLink, onPage);
            Links.Add(value);
            return value;
        }

        public void AddRoot(string absoluteUri)
        {
            Links.Add(new LinkItem(absoluteUri));
        }

        public void Add(string relativeUri, LinkItem onPage)
        {
            var absoluteUrl = new Uri(new Uri(onPage.LinkAbsoluteUri), relativeUri).AbsoluteUri;
            var item = FindOrAdd(absoluteUrl, onPage);
            if (item.OnPage.TryGetValue(onPage.LinkAbsoluteUri, out var count))
            {
                item.OnPage[onPage.LinkAbsoluteUri] = count + 1;
                Console.WriteLine($"Multiple Times = {item.OnPage[onPage.LinkAbsoluteUri]} = on page = {onPage.LinkAbsoluteUri}");
            }
            else
            {
                item.OnPage[onPage.LinkAbsoluteUri] = 1;
            }
        }

        public LinkItem? GetNext()
        {
            var tmp = Links.FirstOrDefault(x => x.StatusCode == -1);
            if (tmp == null || ScrapedLinks.Count >= MaxScrapedPages)
            {
                return null;
            }
            ScrapedLinks.Add(tmp.LinkAbsoluteUri);
            return tmp;
        }
    }
}
