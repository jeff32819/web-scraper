namespace WebScraperDll.Models;

public class LinkItem
{
    public LinkItem(string relativeUri, string pageUri)
    {
        var uri = relativeUri.ToUri(pageUri);
        LinkHost = uri.Host;
        LinkAbsoluteUri = uri.AbsoluteUri;
        var pageHost = pageUri.ToUri().Host;
        var pageAbsoluteUri = pageUri;
        IsInternalLink = string.Equals(pageHost, LinkHost, StringComparison.CurrentCultureIgnoreCase);
    }
    public string LinkHost { get; }
    public string LinkAbsoluteUri { get; }
    public bool IsInternalLink { get; }
}