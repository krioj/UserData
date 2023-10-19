using UserData.Entitys;
using UserData.Repositories;

namespace UserData.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{

}


