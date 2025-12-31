namespace WebScraperDll;

public static class Code
{
    public static bool SameHost(string url1, string url2)
    {
        Console.WriteLine($"url1 = {url1}");
        Console.WriteLine($"url2 = {url2}");
        var u1 = new Uri(url1);
        var u2 = new Uri(url2);

        return u1.Host.Equals(u2.Host, StringComparison.OrdinalIgnoreCase);
    }
}