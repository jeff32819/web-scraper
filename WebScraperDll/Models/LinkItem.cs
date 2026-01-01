using WebRequesterDll.Models;

namespace WebScraperDll.Models
{
    public class LinkItem
    {
        public LinkItem(string absoluteUri)
        {
            LinkAbsoluteUri = absoluteUri.ToAbsoluteUri();
            PageAbsoluteUri = absoluteUri.ToAbsoluteUri();
            VerifyLinksOnly = false;
        }
        public LinkItem(string absoluteUri, LinkItem onPage)
        {
            LinkAbsoluteUri = absoluteUri.ToAbsoluteUri();
            PageAbsoluteUri = onPage.LinkAbsoluteUri;
            VerifyLinksOnly = !string.Equals(LinkAbsoluteUri.GetHost(), PageAbsoluteUri.GetHost(), StringComparison.CurrentCultureIgnoreCase);
        }
        
        public string PageAbsoluteUri { get; }
        public string LinkAbsoluteUri { get; }
        
        public readonly Dictionary<string, int> OnPage = new Dictionary<string, int>();

        /// <summary>
        /// Verify link only, do not scrape content
        /// </summary>
        public bool VerifyLinksOnly { get; }
        public int StatusCode { get;  private set; } = -1;
        public void SetWebResponseResult(WebResponseResult webResponseResult)
        {
            WebResponseResult = webResponseResult;
            StatusCode = webResponseResult.Properties.StatusCode;
        }
        public WebResponseResult? WebResponseResult { get; private set; }
    }
}
