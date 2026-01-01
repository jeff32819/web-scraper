using WebScraperDll.Models;

namespace WebScraperDll
{
    public class PageContainer
    {
        private List<PageItem> Pages { get; set; } = new();

        public void Add(PageItem pg)
        {
            if (Pages.All(p => p.PageUri != pg.PageUri))
            {
                Pages.Add(pg);
            }
        }
    }
}
