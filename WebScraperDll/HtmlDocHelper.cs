using HtmlAgilityPack;

namespace WebScraperDll
{
    public class HtmlDocHelper
    {
        public HtmlDocument Document { get; } = new HtmlDocument();
        public HtmlNodeCollection Links { get; }
        public HtmlDocHelper(string html)
        {
            Document.LoadHtml(html);
            Links = Document.DocumentNode.SelectNodes("//a[@href]");
        }
    }
}
