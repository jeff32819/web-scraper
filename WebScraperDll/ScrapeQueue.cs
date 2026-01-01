using WebScraperDll.Models;

namespace WebScraperDll;

public class ScrapeQueue()
{
    private readonly Queue<PageItem> _queue = new();
    private readonly HashSet<string> _set = new();


    public int Count => _queue.Count;

    public bool Enqueue(PageItem item)
    {
        if (!_set.Add(item.PageUri))
        {
            return false;
        }
        _queue.Enqueue(item);
        return true;
    }

    public PageItem? GetNext() => _queue.Count == 0 ? null : _queue.Dequeue();

    public void Clear()
    {
        _queue.Clear();
        _set.Clear();
    }
}