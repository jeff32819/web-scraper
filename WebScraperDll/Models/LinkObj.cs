namespace WebScraperDll.Models
{
    public class LinkObj(string url)
    {
        public string Host { get; set; } = url.GetHost();
        public string RawUrl { get; set; } = url;
     //   public string Md5 { get; set; } = Jeff32819DLL.MiscCore20.Code.Md5Hash(url.ToAbsoluteUri());
        public string AbsoluteUri { get; set; } = url.ToAbsoluteUri();
    }
}
