using WebRequesterDll;
using WebRequesterDll.Models;
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace WebScraperDll.Models;

public class PageItem(string pageUri)
{
    public string PageHost { get; } = new Uri(pageUri).Host;

    public string PageUri { get; set; } = pageUri;

    public int StatusCode { get; private set; } = -1;
    
    private WebResponseResult? WebResponseResult { get; set; }
    
    public List<LinkItem> Links { get; } = new();

    public async Task GetWebResponseResult()
    {
        WebResponseResult = await Requester.GetFromWeb(PageUri);
        StatusCode = WebResponseResult.Properties.StatusCode;
        var htmlDoc = new HtmlDocHelper(WebResponseResult.Content, PageUri);
        var linkArr = htmlDoc.Links;

        foreach (var link in linkArr)
        {
            var parseLink = ParseLink(link);
            if (string.IsNullOrEmpty(parseLink))
            {
                continue;
            }

            Links.Add(new LinkItem(link, PageUri));
        }


        // maybe return links for processing on the page?
    }

    private string ParseLink(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            return "";
        }

        try
        {
            var absoluteUri = new Uri(new Uri(PageUri), link);
            return absoluteUri.AbsoluteUri;
        }
        catch (UriFormatException)
        {
            return "";
        }
    }
}