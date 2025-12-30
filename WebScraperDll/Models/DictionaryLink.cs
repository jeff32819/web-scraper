using WebRequestDll.Models;

namespace WebScraperDll.Models;

public class DictionaryLink
{
    public IWebResponseResult WebRequest { get; set; } = null!;
    public Dictionary<string, int> OnPage { get; set; } = [];
    public string? LinkHash { get; set; } = null;
}