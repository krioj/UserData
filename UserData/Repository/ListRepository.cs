namespace UserData.Repositories;

using UserData.Entitys;

public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    protected readonly List<T> _items = new();

    public IEnumerable<T> GetAll() => _items.ToList();

    public T? GetById(int id) => _items.Single(item => item.Id == id);

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
    }

    public void Save()
    {
        //Console.WriteLine(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);

    }
}

