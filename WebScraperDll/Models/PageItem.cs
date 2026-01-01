using WebRequesterDll;
using WebRequesterDll.Models;

namespace WebScraperDll.Models;

public class PageItem(string pageUri)
{
    public string PageHost { get; } = new Uri(pageUri).Host;

    public string PageUri { get; set; } = pageUri;

    public int StatusCode { get; private set; } = -1;
    public WebResponseResult? WebResponseResult { get; private set; }
    public List<LinkItem> Links { get; private set; } = new();
    
    public async Task GetWebResponseResult()
    {
        WebResponseResult = await Requester.GetFromWeb(PageUri);
        StatusCode = WebResponseResult.Properties.StatusCode;
        var htmlDoc = new HtmlDocHelper(WebResponseResult.Content, PageUri);
        var linkArr = htmlDoc.Links;

        foreach (var link in linkArr)
        {
            Links.Add(new LinkItem(link, PageUri));
        }
        
        
        
        // maybe return links for processing on the page?
    }
}