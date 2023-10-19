using Microsoft.EntityFrameworkCore;
using UserData.Entitys;
using UserData.Repositories;

namespace UserData.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        //private readonly Action<T>? _itemAddedCallback;

        public SqlRepository(DbContext dbContext)//, Action<T>? itemAddedCallback = null)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            //_itemAddedCallback = itemAddedCallback;
        }

        public event EventHandler<T>? ItemAdded;

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T GetById(int id) => _dbSet.Find(id);

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);

        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);


        }
        public void Save() => _dbContext.SaveChanges();
    }
}




