namespace WebScraperDll.Models
{
    public class LinkObj(string url)
    {
        public string Host { get; set; } = url.GetHost();
        public string RawUrl { get; set; } = url;
        public string AbsoluteUri { get; set; } = url.ToAbsoluteUri();
    }
}
