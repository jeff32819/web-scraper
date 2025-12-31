namespace WebScraperDll
{
    public static class Ext
    {
        public static bool TryFind<T>(this List<T> list, Func<T, bool> predicate, out T value)
        {

            value = list.FirstOrDefault(predicate);
            return value != null;
        }

        public static string NormalizeUri(string txt)
        {
            var uri = new Uri(txt);
            return uri.AbsoluteUri;
        }
        
    }
}
