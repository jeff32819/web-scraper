using Newtonsoft.Json.Linq;

using WebScraperDll.Models;

namespace WebScraperDll
{
    public class LinkList
    {
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

            value = new LinkItem(scrapedLink, new Uri(scrapedLink));
            Links.Add(value);
            return value;
        }

        public void AddRoot(string absoluteUri)
        {
            Links.Add(new LinkItem(absoluteUri, new Uri(absoluteUri)));
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
        public LinkItem? GetNext() => Links.FirstOrDefault(x => x.NeedToSrape);
    }
}
