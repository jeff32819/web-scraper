using WebScraperDll.Models;

namespace WebScraperDll;

public class ScrapeQueue()
{
    private readonly Queue<string> _queue = new();
    private readonly HashSet<string> _hashSet = new();


    public int Count => _queue.Count;

    public bool Enqueue(string item)
    {
        if (!_hashSet.Add(item))
        {
            return false;
        }
        _queue.Enqueue(item);
        return true;
    }

    public string? GetNext() => _queue.Count == 0 ? null : _queue.Dequeue();

    public void Clear()
    {
        _queue.Clear();
        _hashSet.Clear();
    }
}