using WebRequesterDll;

namespace WebScraperDll;

public static class Code
{
    public static async Task<List<HtmlDocHelper.LinkItem>> GetFromWeb(string uri)
    {
        var request = await Requester.GetFromWeb(uri);
        var htmlDoc = new HtmlDocHelper(request.Content, uri);
        var links = htmlDoc.Links;
        return links;
    }
}