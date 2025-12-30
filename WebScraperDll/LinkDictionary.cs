using WebRequestDll.Models;

using WebScraperDll.Models;

namespace WebScraperDll;

public class LinkDictionary
{
    public Dictionary<string, DictionaryLink> Links { get; } = new();

    /// <summary>
    ///     Link that is being scraped
    /// </summary>
    /// <param name="scrapedLink"></param>
    /// <returns></returns>
    private DictionaryLink FindOrAdd(string scrapedLink)
    {
        if (Links.TryGetValue(scrapedLink, out var value))
        {
            return value;
        }

        value = new DictionaryLink();
        Links[scrapedLink] = value;
        return value;
    }

    /// <summary>
    ///     Get the link that is being scraped, returns null if it does not exist
    /// </summary>
    /// <param name="scrapedLink"></param>
    /// <returns></returns>
    public DictionaryLink? Get(string scrapedLink)
    {
        Links.TryGetValue(scrapedLink, out var value);
        return value;
    }

    public DictionaryLink Add(IWebResponseResult webRequest)
    {
        var item = FindOrAdd(webRequest.Properties.FinalUrl);
        item.LinkHash = Jeff32819DLL.MiscCore20.Code.Md5Hash(webRequest.Properties.FinalUrl);
        item.WebRequest = webRequest;
  //      item.OnPage();
        return item;
    }

    public DictionaryLink AddLinkForPage(IWebResponseResult webRequest)
    {
        var item = FindOrAdd(webRequest.Properties.FinalUrl);
        //if (item.OnPage.TryGetValue(webRequest.Properties.FinalUrl, out var count))
        //{
        //    item.OnPage[webRequest.Properties.FinalUrl] = count + 1;
        //    Console.WriteLine($"Multiple Times = {item.OnPage[webRequest.Properties.FinalUrl]} = on page = {scrapedLink}");
        //}
        //else
        //{
        //    item.WebRequest = webRequest;
        //    item.LinkHash = Jeff32819DLL.MiscCore20.Code.Md5Hash(scrapedLink);
        //    item.OnPage[pageUrl] = 1;
        //}

        return item;
    }

    /// <summary>
    ///     Determines whether a scraped link already exists in the dictionary.
    /// </summary>
    /// <param name="scrapedLink">The scraped link to check.</param>
    /// <returns>True if the link exists; otherwise false.</returns>
    public bool Exists(string scrapedLink)
    {
        return !string.IsNullOrWhiteSpace(scrapedLink) && Links.ContainsKey(scrapedLink);
    }
}