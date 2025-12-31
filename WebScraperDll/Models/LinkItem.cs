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

        public int StatusCode { get;  private set; } = -1;
        public bool SkipScrape { get; set; }
        public bool NeedToSrape => !SkipScrape && StatusCode == -1;
        public void SetWebResponseResult(WebResponseResult webResponseResult)
        {
            WebResponseResult = webResponseResult;
            StatusCode = webResponseResult.Properties.StatusCode;
        }
        public WebResponseResult? WebResponseResult { get; private set; }
    }
}
