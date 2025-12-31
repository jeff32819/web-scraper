namespace WebScraperDll.Models
{
    public class PageItem
    {
        public PageItem(string absoluteUri)
        {
            Uri = new Uri(absoluteUri);
            Md5 = Jeff32819DLL.MiscCore20.Code.Md5Hash(Uri.AbsoluteUri);
        }
        public string Md5 { get; }
        public Uri Uri { get; }
    }
}
