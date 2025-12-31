using WebRequesterDll.Models;

namespace WebScraperDll.Models
{
    public class LinkItem
    {
        public LinkItem(string absoluteUri)
        {
            Uri = new Uri(absoluteUri);
        }
        public Uri Uri { get; }
        
        public readonly Dictionary<string, int> OnPage = new Dictionary<string, int>();
        /// <summary>
        /// Has the link been scraped from the web.
        /// </summary>
        public bool LinkScrapedFromWeb { get;  private set; }
        public bool SkipScrape { get; set; }
        public bool NeedToSrape => !SkipScrape && !LinkScrapedFromWeb;
        public void SetWebResponseResult(WebResponseResult webResponseResult)
        {
            WebResponseResult = webResponseResult;
            LinkScrapedFromWeb = true;
        }
        public WebResponseResult? WebResponseResult { get; private set; }
    }
}
