using WebScraperDll.Models;

namespace WebScraperDll
{
    public class PageContainer
    {
        private List<PageItem> Pages { get; set; } = new();

        public PageItem Add(string pageUrl)
        {
            var pg = new PageItem(pageUrl);
            if (Pages.All(p => p.PageUri != pg.PageUri))
            {
                Pages.Add(pg);
            }
            return pg;
        }
    }
}
