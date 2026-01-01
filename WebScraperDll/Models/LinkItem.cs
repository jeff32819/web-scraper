namespace WebScraperDll.Models;

public class LinkItem
{
    public LinkItem(string relativeUri, string pageUri, string pageHost)
    {
        var uri = relativeUri.ToUri(pageUri);
        LinkHost = uri.Host;
        LinkAbsoluteUri = uri.AbsoluteUri;
        PageHost = pageHost;
        PageAbsoluteUri = pageUri;
    }
    public string PageHost { get; }
    public string LinkHost { get; }
    public string PageAbsoluteUri { get; }
    public string LinkAbsoluteUri { get; }
    public bool IsInternalLink => string.Equals(PageHost, LinkHost, StringComparison.CurrentCultureIgnoreCase);
}