namespace WebScraperDll
{
    public static class Ext
    {
        public static bool TryFind<T>(this List<T> list, Func<T, bool> predicate, out T value)
        {

            value = list.FirstOrDefault(predicate);
            return value != null;
        }

        public static string ToAbsoluteUri(this string txt)
        {
            var uri = new Uri(txt);
            return uri.AbsoluteUri;
        }
        public static string GetHost(this string txt)
        {
            var uri = new Uri(txt);
            return uri.Host;
        }

    }
}
