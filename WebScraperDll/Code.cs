using WebRequesterDll;
using WebScraperDll.Models;

namespace WebScraperDll;

public static class Code
{
    public static async Task<WebRequest> GetFromWeb(string uri)
    {
        var request = await Requester.GetFromWeb(uri);
        var htmlDoc = new HtmlDocHelper(request.Content, uri);

        return new WebRequest
        {
            WebResponseResult = request,
            HtmlDocHelper = htmlDoc
        };
    }
}