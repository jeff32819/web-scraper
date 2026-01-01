using System.Diagnostics;

namespace WebScraperDll
{
    public static class Ext
    {
        public static bool TryFind<T>(this List<T> list, Func<T, bool> predicate, out T? value)
        {

            value = list.FirstOrDefault(predicate);
            return value != null;
        }

        public static string ToAbsoluteUri(this string txt)
        {
            Debug.Print($"Converting to absolute URI: {txt}");
            var uri = new Uri(txt);
            return uri.AbsoluteUri;
        }
        public static string GetHost(this string txt)
        {
            var uri = new Uri(txt);
            return uri.Host;
        }

        public static Uri ToUri(this string txt)
        {
            try
            {
                return new Uri(txt);
            }
            catch
            {
                throw new Exception($"Failed to convert '{txt}' to Uri");
            }
        }
        public static Uri ToUri(this string txt, string pageUrl)
        {
            try
            {
                return new Uri(new Uri(pageUrl), txt);
            }
            catch
            {
                throw new Exception($"Failed to convert '{txt}' to Uri");
            }
        }
        public static string ToMd5(this string txt) => Jeff32819DLL.MiscCore20.Code.Md5Hash(txt);

        public static bool TryPopFirst<T>(this List<T> list, out T value)
        {
            if (list.Count > 0)
            {
                value = list[0];
                list.RemoveAt(0);
                return true;
            }

            value = default!;
            return false;
        }
    }
}
