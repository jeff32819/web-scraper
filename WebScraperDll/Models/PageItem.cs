using WebRequesterDll;
using WebRequesterDll.Models;
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace WebScraperDll.Models;

public class PageItem(string pageUri)
{
    public string PageHost { get; } = new Uri(pageUri).Host;

    public string PageUri { get; set; } = pageUri;

    public int StatusCode { get; private set; } = -1;
    public WebResponseResult? WebResponseResult { get; private set; }
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

    public string ParseLink(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            return "";
        }

        // Attempt to parse the link and make it absolute if it's relative
        try
        {
            var absoluteUri = new Uri(new Uri(PageUri), link);
            return absoluteUri.AbsoluteUri;
        }
        catch (UriFormatException)
        {
            // Handle cases where the link is not a valid URI format
            return "";
        }
    }
}