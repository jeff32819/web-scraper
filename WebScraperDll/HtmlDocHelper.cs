using HtmlAgilityPack;

namespace WebScraperDll
{
    public class HtmlDocHelper
    {
        public HtmlDocument Document { get; } = new();
        public HtmlDocHelper(string html, string pageAbsoluteUri)
        {
            Console.WriteLine($"HtmlDocHelper = pageAbsoluteUri = {pageAbsoluteUri}");
            var pageUri = pageAbsoluteUri.ToUri();
            Host = pageUri.Host;
            Document.LoadHtml(html);
            var links = Document.DocumentNode.SelectNodes("//a[@href]");
            if (links == null || !links.Any())
            {
                // log no links found
                return;
            }
            foreach (var linkUri in links.Select(item => item.GetAttributeValue("href", string.Empty)).Select(href => new Uri(new Uri(pageAbsoluteUri), href)))
            {
                AllLinks.Add(linkUri.AbsoluteUri);
                if (string.Equals(pageUri.Host, linkUri.Host, StringComparison.CurrentCultureIgnoreCase))
                {
                    InternalLinks.Add(linkUri.AbsoluteUri);
                }
                else
                {
                    ExternalLinks.Add(linkUri.AbsoluteUri);
                }
            }
        }
        public string Host { get; }
        public List<string> AllLinks { get; set; } = new();
        public List<string> InternalLinks { get; set; } = new();
        public List<string> ExternalLinks { get; set; } = new();

    }
}
