using WebScraperDll.Models;

namespace WebScraperDll
{
    public class LinkContainer()
    {
        public List<LinkItem> Links { get; } = new();


        /// <summary>
        ///     Link that is being scraped
        /// </summary>
        /// <param name="scrapedLink"></param>
        /// <param name="onPage"></param>
        /// <returns></returns>
        private LinkItem FindOrAdd(string scrapedLink, PageItem onPage)
        {
            if (Links.TryFind(x => x.LinkAbsoluteUri == scrapedLink, out var value))
            {
                return value;
            }
       //     value = new LinkItem(scrapedLink, onPage);
            Links.Add(value);
            return value;
        }

        //public void AddRoot(string absoluteUri)
        //{
        //    Links.Add(new LinkItem(absoluteUri));
        //}

        //public void Add(string relativeUri, PageItem onPage)
        //{
        //    var absoluteUrl = new Uri(new Uri(onPage.PageUri), relativeUri).AbsoluteUri;
        //    var item = FindOrAdd(absoluteUrl, onPage);
        //    if (item.OnPage.TryGetValue(onPage.PageUri, out var count))
        //    {
        //        item.OnPage[onPage.PageUri] = count + 1;
        //        Console.WriteLine($"Multiple Times = {item.OnPage[onPage.PageUri]} = on page = {onPage.PageUri}");
        //    }
        //    else
        //    {
        //        item.OnPage[onPage.PageUri] = 1;
        //    }
        //}
    }
}
