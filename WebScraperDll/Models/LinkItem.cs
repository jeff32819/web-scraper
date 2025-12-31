using System.Security.Cryptography;
using HtmlAgilityPack;
using WebRequesterDll.Models;

namespace WebScraperDll.Models
{
    public class LinkItem
    {
        public LinkItem(string absoluteUri)
        {
            AbsoluteUri = absoluteUri;
            Uri = new Uri(absoluteUri);
            Md5 = Jeff32819DLL.MiscCore20.Code.Md5Hash(absoluteUri);
        }
        public string Md5 { get; }
        public Uri Uri { get; }
        
        public string AbsoluteUri { get; }
        public Dictionary<string, int> OnPage = new Dictionary<string, int>();
        /// <summary>
        /// Has the link been scraped from the web.
        /// </summary>
        public bool LinkScrapedFromWeb { get; set; }
        public bool SkipScrape { get; set; }
        public bool NeedToSrape => !SkipScrape && !LinkScrapedFromWeb;
        public WebResponseResult? WebResponseResult { get; set; }
        public List<string> Links { get; set; } = new();
    }
    
}
