using HtmlAgilityPack;

namespace WebScraperDll
{
    public class HtmlDocHelper
    {
        public HtmlDocument Document { get; } = new();
        public HtmlDocHelper(string html, string pageAbsoluteUri)
        {
            var pageUri = new Uri(pageAbsoluteUri);
            Document.LoadHtml(html);
            foreach (var linkUri in Document.DocumentNode.SelectNodes("//a[@href]").Select(item => item.GetAttributeValue("href", string.Empty)).Select(href => new Uri(new Uri(pageAbsoluteUri), href)))
            {
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
        
        public List<string> InternalLinks { get; set; } = new();
        public List<string> ExternalLinks { get; set; } = new();

    }
}
