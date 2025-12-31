using WebRequesterDll.Models;

namespace WebScraperDll.Models
{
    public class LinkToScrape(string absoluteUri)
    {
        public string AbsoluteUri { get; set; } = absoluteUri;
        public List<LinkObj> Links { get; set; } = new();
        public WebResponseResult? WebResponseResult { get; set; }
    }
}
