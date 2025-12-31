using WebRequesterDll.Models;

namespace WebScraperDll.Models
{
    public class LinkItem
    {
        public LinkItem(string absoluteUri, Uri pageUri)
        {
            AbsoluteUri = absoluteUri.ToAbsoluteUri();
            PageAbsoluteUri = pageUri.AbsoluteUri;
            SameHost = string.Equals(AbsoluteUri.GetHost(), PageAbsoluteUri.GetHost(), StringComparison.CurrentCultureIgnoreCase);
        }
        
        public string PageAbsoluteUri { get; }
        public string AbsoluteUri { get; }
        
        public readonly Dictionary<string, int> OnPage = new Dictionary<string, int>();

        public int StatusCode { get;  private set; } = -1;
        public bool SameHost { get; }
        public bool NeedToSrape => SameHost && StatusCode == -1;
        public void SetWebResponseResult(WebResponseResult webResponseResult)
        {
            WebResponseResult = webResponseResult;
            StatusCode = webResponseResult.Properties.StatusCode;
        }
        public WebResponseResult? WebResponseResult { get; private set; }
    }
}
